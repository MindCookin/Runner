using UnityEngine;
using System.Collections;

public class TitleText : MonoBehaviour {
	
	public int offset;
	
	PlayerMove player;
	
	// Use this for initialization
	void Awake () {
	
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		
		GameEventManager.GameInit 	+= GameInit;
		GameEventManager.GameStart 	+= GameStart;
		GameEventManager.GameOver 	+= GameOver;
	}
	
	// Update is called once per frame
	void Update () {
		
		if( player.DistanceTraveled > offset )
			enabled = false;
		
		transform.Rotate( 0, 2, 0 );
	}
	
	void GameInit() {
		
		transform.rotation = Quaternion.identity;
		renderer.enabled = true;
		enabled 		 = true;
	}
	
	void GameStart() {
		
	}
	
	void GameOver() {
		
	}
}
