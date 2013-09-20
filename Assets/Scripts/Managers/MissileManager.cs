using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileManager : MonoBehaviour {
	
	public Transform missile;
	public int variableY, recycleOffset, quantity, separationBetweenMissiles;
	public Vector3 startingPos;
	
	private Quaternion initialRotation;
	private Queue<Transform> missileQueue;
	private PlayerMove player;
	private int lastMissileX;
	
	void Awake() {
	
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;	
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		missileQueue = new Queue<Transform>( quantity );
		
		for ( int i = 0; i < quantity; i++ )
		{
			Transform queuedObject = (Transform) Instantiate( missile );
			queuedObject.parent = transform;
			queuedObject.gameObject.SetActive(false);
			initialRotation = queuedObject.rotation;
			missileQueue.Enqueue( queuedObject );
		}
		
		enabled = false;
	}
	
	void Update () {
		
		Transform obj = missileQueue.Peek();
		
		if (obj.localPosition.x + recycleOffset < player.DistanceTraveled )
			Remove();
	
		if( player.DistanceTraveled > lastMissileX
			&& Mathf.FloorToInt(player.DistanceTraveled) % separationBetweenMissiles == 0 
			&& Random.value < LevelStateManager.GetInstance().MissilePercent )
			Add();
	}
	
	void Remove() {
	
		Transform targetObject = missileQueue.Dequeue();
		targetObject.rigidbody.Sleep();
		targetObject.gameObject.SetActive(false);	
		missileQueue.Enqueue( targetObject );
	}
	
	void Add() {
		
		Transform targetObject = missileQueue.Dequeue();
		
		Vector3 targetPosition 	= startingPos;
		targetPosition.x		+= player.DistanceTraveled;
		targetPosition.y 		+= Random.value * variableY - variableY/2;
		
		targetObject.transform.rotation = initialRotation;
		targetObject.transform.position = targetPosition;
		
		targetObject.gameObject.SetActive( true );
		missileQueue.Enqueue( targetObject );
		
		lastMissileX = Mathf.FloorToInt(player.DistanceTraveled) + 1;
	}
	
	void GameStart () {
		
		lastMissileX = 0;
		
		enabled = true;
	}
	
	void GameOver () {
		enabled = false;
		Remove();
	}
}
