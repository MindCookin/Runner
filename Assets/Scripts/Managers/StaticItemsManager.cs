using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticItemsManager : MonoBehaviour
{
	public Vector3 startingPosition;
	public int recycleOffset;
	public Transform coin;
	
	private Vector3 nextPosition;
	private Queue<Transform> coinQueue;
	private int queuedQuantity = 10;
	
	private PlayerMove player;
	
	void Awake() {
		
		nextPosition = startingPosition;
	
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;	
		GameEventManager.GameInit += GameInit;	
	
		player 		= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		
		coinQueue	= new Queue<Transform>( queuedQuantity );
		for( int i = 0; i < queuedQuantity; i++ )
		{
			Transform queuedObject = (Transform) Instantiate( coin );
			queuedObject.parent = transform;
			coinQueue.Enqueue( queuedObject );
		}
		
		enabled = false;
	}
	
	void Update (){
		
		// check if objectPosition is higher than our offset (i.e. is off screen);
		if ( nextPosition.x + recycleOffset < player.DistanceTraveled )
			Recycle();
	}
	
	void Recycle() {
		
		nextPosition.x += startingPosition.x;
			
		nextPosition = CoinDistribution.RandomDistribution( coinQueue, nextPosition );
	}
	
	void GameStart () {
		
		enabled = true;
	}
	
	void GameInit() {
		
		nextPosition 		= startingPosition;
		
		Recycle();
	}
	
	void GameOver () {
		enabled = false;
	}
}