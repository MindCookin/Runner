using UnityEngine;
using System.Collections;

public class PlatformEnemies : MonoBehaviour
{
	public Transform[] prefabs;
	private Transform[] enemies;
	
	int index;
	GameObject enemyContainer;
		
	void Start()
	{
		enemyContainer = GameObject.Find("EnemyContainer");
		
		enemies = new Transform[ prefabs.Length ];
		
		for ( int i = 0; i < prefabs.Length; i++ )
		{
			Transform enemy	= (Transform)Instantiate( prefabs[i] );
			enemy.transform.parent  = enemyContainer.transform;
			enemy.gameObject.SetActive( false );
			
			enemies[i] = enemy;
		}
	}
	
	public void TryToAddEnemy(){
			
		index = -1;
		
		if( Random.value < LevelStateManager.GetInstance().EnemyPercent )
		{
			index = Random.Range( 0, enemies.Length );
			Transform enemy = enemies[ index ];
			
			enemy.gameObject.SetActive(true);
			enemy.GetComponent<EnemyPlace>().Place( transform );
			
			PlayerDataManager.AddToValue( SessionData.ENEMIES, 1 );
		}
		
		for ( int i = 0; i < enemies.Length; i++ )
			if ( i != index )
				enemies[i].gameObject.SetActive(false);
	}
	
	public void RemoveAll() {
		
		for ( int i = 0; i < enemies.Length; i++ )
			enemies[i].gameObject.SetActive(false);
	}
}

