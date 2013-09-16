using UnityEngine;
using System.Collections;

public class Platform : Entity
{
	public Transform enemyPrefab;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY, enemyPercent;
	
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;
	
	private EnemyPlace enemyPlace;
	private Transform enemy;
	
	public override void Place (Vector3 nextPosition)
	{
		// setup platform size
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));
		
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
			Random.Range(minGap.x, maxGap.x),
			Random.Range(minGap.y, maxGap.y),
			Random.Range(minGap.z, maxGap.z));
		
		// adjust nextPosition y for minY and maxY
		if(mNextPosition.y < minY)
			mNextPosition.y = minY + maxGap.y;
		else if(mNextPosition.y > maxY)
			mNextPosition.y = maxY - maxGap.y;
		
		if( Random.value < enemyPercent )
		{
			if( enemyPlace == null )
				CreateEnemy();
			
			enemyPlace.Place( transform );
		}
	}
	
	private void CreateEnemy(){
		enemy = (Transform)Instantiate(enemyPrefab);
		enemyPlace = enemy.GetComponent<EnemyPlace>();
		enemy.parent = transform.parent;
	}
}
