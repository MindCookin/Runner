using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {
	
//	public float enemyProbability, enemyMinSize, enemyMaxSize;
	public int queuedQuantity;
	public int recycleOffset;
	
	public Transform platform;
	public Transform blockingPlatform;

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
		
		blockingPlatform.localScale = new Vector3( 1, 100, 1 );
		
		enabled = false;
	}
	
	void Update (){
		
		Transform obj = platformQueue.Peek();
		
		// check if objectPosition is higher than our offset (i.e. is off screen);
		if (obj.localPosition.x + recycleOffset < player.DistanceTraveled )
		{
			Recycle();
			RecycleBlockingPlatform();
		}
	}
	
	void Recycle() {
		
		Transform targetObject = platformQueue.Dequeue();
		
		platformScript = targetObject.GetComponent<Platform>();
			
		platformScript.Place( nextPosition );
		nextPosition = platformScript.NextPosition;
			
		platformQueue.Enqueue( targetObject );
	}
	
	void RecycleBlockingPlatform() {
		
		if( !blockingPlatform.gameObject.activeSelf )
			blockingPlatform.gameObject.SetActive( true );
			
		Transform lastPlatform = platformQueue.Peek();	
		blockingPlatform.position = lastPlatform.position;
		blockingPlatform.Translate( -lastPlatform.localScale.x / 2 - 0.5f, blockingPlatform.localScale.y/2 - 0.5f, 0 );
	}
	
	void GameStart () {
		
		enabled = true;
	}
	
	void GameInit () {
		
		nextPosition = Vector3.zero;
		
		for( int i = 0; i < queuedQuantity; i++ )
			Recycle();
		
		RecycleBlockingPlatform();
		
		blockingPlatform.gameObject.SetActive( false );
	}
	
	void GameOver () {
		
		enabled = false;
	}
}
