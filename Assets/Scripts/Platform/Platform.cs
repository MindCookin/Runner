using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
	protected Vector3 mNextPosition = Vector3.zero;
	
	public Transform enemySliderPrefab, enemyBouncerPrefab;
//	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;
	
	private Transform[] enemies;
	
	Vector3 size;
	
	LevelStateManager level;
	
	void Awake() {
		
		size = Vector3.one;
		
		level = LevelStateManager.instance;
		
		// listen to game events
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameInit += GameInit;
		GameEventManager.GameOver += GameOver;
		
		renderer.enabled	= false;
		enabled 			= false;
		
		GameObject enemyContainer = GameObject.Find("EnemyContainer");
		
		Transform slider 	= (Transform)Instantiate(enemySliderPrefab);
		Transform bouncer 	= (Transform)Instantiate(enemyBouncerPrefab);
		
		slider.transform.parent  = enemyContainer.transform;
		bouncer.transform.parent = enemyContainer.transform;
		
		slider.gameObject.SetActive( false );
		bouncer.gameObject.SetActive( false );
		
		enemies = new Transform[2] { slider, bouncer };
	}
	
	public void Place (Vector3 nextPosition)
	{
		// setup platform size
		Vector3 scale = new Vector3(
			Random.Range(size.x, 50),
			size.y,
			size.z);
		
		// setup platform position
		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		
		// update platform size and position
		transform.localScale = scale;
		transform.localPosition = position;
		mNextPosition.x = nextPosition.x + scale.x;
		
		// setup material
		int materialIndex = Random.Range(0, materials.Length);
		renderer.material = materials[materialIndex];
		collider.material = physicMaterials[materialIndex];
		
		// generate a gap and add it to nextPosition
		mNextPosition += new Vector3(
			Random.Range(level.PlatformMinXGap, level.PlatformMaxXGap ),
			Random.Range(level.PlatformMinYGap, level.PlatformMaxYGap),
			0 );
		
		// adjust nextPosition y for minY and maxY
		if(mNextPosition.y < minY)
			mNextPosition.y = minY + level.PlatformMaxXGap;
		else if(mNextPosition.y > maxY)
			mNextPosition.y = maxY - level.PlatformMaxYGap;
		
		if( Random.value < level.EnemyPercent )
			AddEnemy();
	}
	
	private void AddEnemy(){
	
		for ( int i = 0; i < enemies.Length; i++ )
			enemies[i].gameObject.SetActive( false );
			
		Transform enemy = enemies[ Random.Range( 0, enemies.Length ) ];
		enemy.GetComponent<EnemyPlace>().Place( transform );
	}
	
	void GameStart() {
		// enable to play
		renderer.enabled	= true;
		enabled 			= true;
	}
	
	void GameOver() {
		// disable on gameover
		renderer.enabled	= false;
		enabled 			= false;
	}
	
	void GameInit() {
		
		renderer.enabled	= true;
	}
	
	public Vector3 NextPosition{ get { return mNextPosition; } }
}
