using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
	protected Vector3 mNextPosition = Vector3.zero;
	
	public Transform enemySliderPrefab, enemyBouncerPrefab, capsulePrefab;
//	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;
	
	private Transform[] enemies;
	Transform capsule;
	
	void Awake() {
		
		GameObject enemyContainer = GameObject.Find("EnemyContainer");
		GameObject itemsContainer = GameObject.Find("DropItemsContainer");
		
		Transform slider 	= (Transform)Instantiate(enemySliderPrefab);
		Transform bouncer 	= (Transform)Instantiate(enemyBouncerPrefab);
		capsule				= (Transform)Instantiate(capsulePrefab);
		
		slider.transform.parent  = enemyContainer.transform;
		bouncer.transform.parent = enemyContainer.transform;
		capsule.transform.parent = itemsContainer.transform;
		
		slider.gameObject.SetActive( false );
		bouncer.gameObject.SetActive( false );
		capsule.gameObject.SetActive( false );
		
		enemies = new Transform[2] { slider, bouncer };
	}
	
	public void Place (Vector3 nextPosition)
	{
		// setup platform size
		Vector3 scale = new Vector3(
			Random.Range(LevelStateManager.GetInstance().PlatformMinSize, LevelStateManager.GetInstance().PlatformMaxSize ),
			1,
			1);
		
		// generate a gap and add it to nextPosition
		Vector3 gap = new Vector3(
			Random.Range(LevelStateManager.GetInstance().PlatformMinXGap, LevelStateManager.GetInstance().PlatformMaxXGap ),
			Random.Range(LevelStateManager.GetInstance().PlatformMinYGap, LevelStateManager.GetInstance().PlatformMaxYGap),
			0 );
		
		// setup platform position
		Vector3 position = nextPosition;
		position.x += gap.x + scale.x/2;
		position.y = gap.y; 
		
		// update platform size and position
		transform.localScale = scale;
		transform.localPosition = position;
		
		mNextPosition = position;
		mNextPosition.x += scale.x/2;
		
		// setup material
		int materialIndex = Random.Range(0, materials.Length);
		renderer.material = materials[materialIndex];
		collider.material = physicMaterials[materialIndex];
		
		if( Random.value < LevelStateManager.GetInstance().EnemyPercent )
			AddEnemy();
		
		if( Random.value < LevelStateManager.GetInstance().DropPickerPercent )
			AddCapsule();
	}
	
	private void AddEnemy(){
	
		for ( int i = 0; i < enemies.Length; i++ )
			enemies[i].gameObject.SetActive( false );
			
		Transform enemy = enemies[ Random.Range( 0, enemies.Length ) ];
		enemy.GetComponent<EnemyPlace>().Place( transform );
	}
	
	private void AddCapsule() {
		
		capsule.GetComponent<PickupItem>().Reset();
		capsule.GetComponent<PlatformPickupPlace>().Place( transform );
	}
	
	public Vector3 NextPosition{ get { return mNextPosition; } }
}
