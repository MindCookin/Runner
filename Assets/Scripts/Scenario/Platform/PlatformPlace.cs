using UnityEngine;
using System.Collections;

public class PlatformPlace : MonoBehaviour
{
	protected Vector3 mNextPosition = Vector3.zero;
	
	public float minY, maxY;
	
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;
	
	PlatformCapsule capsuleScript;
	PlatformEnemies enemyScript;
	PlatformRigidbody rigidbodyScript;
	
	void Start() {
		
		enemyScript 	= GetComponent<PlatformEnemies>();
		capsuleScript	= GetComponent<PlatformCapsule>();
		rigidbodyScript = GetComponent<PlatformRigidbody>();
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
		
		capsuleScript.TryToAddCapsule();
		enemyScript.TryToAddEnemy();
		rigidbodyScript.TryToAddRigidBody();
	}
	
	public void RemoveItems() {
		
		enemyScript.RemoveAll();
		capsuleScript.Remove();
	}
	
	public Vector3 NextPosition{ get { return mNextPosition; } }
}
