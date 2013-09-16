using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {
	
	public float enemyProbability;
	public int queuedQuantity;
	public int recycleOffset;
	
	public Transform platform;

	private Vector3 nextPosition = Vector3.zero;
	private Queue<Transform> platformQueue;
	
	private PlayerMove player;
	
	void Awake() {
	
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;	
	
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
		
		Entity entity = targetObject.GetComponent<Entity>();
		entity.Place( nextPosition );
		nextPosition = entity.NextPosition;
		
		platformQueue.Enqueue( targetObject );
	}
	
	void GameStart () {
		
		nextPosition = Vector3.zero;
		
		for( int i = 0; i < queuedQuantity; i++ )
			Recycle();
		
		enabled = true;
	}
	
	void GameOver () {
		enabled = false;
	}
}
