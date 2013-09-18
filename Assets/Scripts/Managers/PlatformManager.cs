using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {
	
//	public float enemyProbability, enemyMinSize, enemyMaxSize;
	public int queuedQuantity;
	public int recycleOffset;
	
	public Transform platform;

	private Vector3 nextPosition = Vector3.zero;
	private Queue<Transform> platformQueue;
	
	private PlayerMove player;
	private Platform platformScript;
	
	void Awake() {
	
		GameEventManager.GameInit 	+= GameInit;
		GameEventManager.GameStart 	+= GameStart;
		GameEventManager.GameOver 	+= GameOver;	
	
		platformQueue	= new Queue<Transform>( queuedQuantity );
		player 	= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		
		for( int i = 0; i < queuedQuantity; i++ )
		{
			Transform queuedObject = (Transform) Instantiate( platform );
			queuedObject.parent = transform;
			platformQueue.Enqueue( queuedObject );
		}
		
		enabled = false;
	}
	
	void Update (){
		
		Transform obj = platformQueue.Peek();
		
		// check if objectPosition is higher than our offset (i.e. is off screen);
		if (obj.localPosition.x + recycleOffset < player.DistanceTraveled )
			Recycle();
	}
	
	void Recycle() {
		
		Transform targetObject = platformQueue.Dequeue();
		
		platformScript = targetObject.GetComponent<Platform>();
			
		platformScript.Place( nextPosition );
		nextPosition = platformScript.NextPosition;
			
		platformQueue.Enqueue( targetObject );
	}
	
	void GameStart () {
		
		enabled = true;
	}
	
	void GameInit () {
		
		nextPosition = Vector3.zero;
		
		for( int i = 0; i < queuedQuantity; i++ )
			Recycle();
	}
	
	void GameOver () {
		enabled = false;
	}
}
