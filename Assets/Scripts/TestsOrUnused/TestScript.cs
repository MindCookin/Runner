using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {
	
	public Transform platform, enemy, missile;
	public Vector3 platformPosition;
	
	void Start () {
		
		
		platform = (Transform)Instantiate(platform);
		enemy	 = (Transform)Instantiate(enemy);
		
		platform.GetComponent<Platform>().Place( platformPosition ); 
		enemy.GetComponent<EnemyPlace>().Place( platform );
		
		GameEventManager.TriggerGameStart();
	}
	
	void Update() {
		
	}
}
