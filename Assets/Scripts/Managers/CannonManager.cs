using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonManager : MonoBehaviour {
	
	public float spawnItemPercentage;
	
	public Transform cannonColumn;
	public int recycleOffset;
	public int numberOfColumns;
	public int distanceBetweenColumns;
	
	private Runner runner;
	private int nextPosition;
	
	private Queue<Transform> cannonQueue;
	
	void Awake(){
		
		// find player and initialize Runner
		GameObject rnr = GameObject.FindGameObjectWithTag("Player");
		runner = rnr.GetComponent<Runner>();
	
		// listen to game events
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;		
	}
	
	void Start() {
		
		cannonQueue 		= new Queue<Transform>(numberOfColumns);
		GameObject player 	= GameObject.FindGameObjectWithTag("Player");
		runner 				= player.GetComponent<Runner>();
		
		for( int i = 0; i < numberOfColumns; i++)
			cannonQueue.Enqueue( (Transform)Instantiate(cannonColumn) );   
		
		enabled = false;
	}
	
	void Update () {
		
		float dist = cannonQueue.Peek().localPosition.x + recycleOffset;
		
		if ( dist < runner.DistanceTraveled )
			RecycleCannons();
	}
	
	void RecycleCannons() {
		
		// recycle cannon column 
		Transform o = cannonQueue.Dequeue();
		
		CannonColumn script = o.GetComponent<CannonColumn>();
		script.update();
		o.position = new Vector3( nextPosition, 0, 0 );
		
		cannonQueue.Enqueue(o);
		
		// test item creation
		if ( spawnItemPercentage < Random.value )
			Spawn();
		
		nextPosition += distanceBetweenColumns;
	}
	
	void Spawn() {
		////// TODO
	}
	
	void GameStart() {
	
		// set starting position
		nextPosition = 0;
		
		// locate objects
		for( int i = 0; i < numberOfColumns; i++ )
			RecycleCannons();
		
		// enable to play
		enabled = true;	
	}
	
	void GameOver() {
		enabled = false;	
	}
}
