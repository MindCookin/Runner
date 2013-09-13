using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticItemsManager : MonoBehaviour
{
	public Vector3 startingPosition;
	public int recycleOffset;
	public Transform coin, cannon, pickup;

	private Vector3 nextPosition, lastCoinPosition;
	private Queue<Transform> coinQueue;
	private int queuedQuantity = 10;
		
	private Runner player;
	
	void Awake() {
		
		nextPosition = startingPosition;
	
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;	
	
		coinQueue	= new Queue<Transform>( queuedQuantity );
		player 		= GameObject.FindGameObjectWithTag("Player").GetComponent<Runner>();
		
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
		if ( lastCoinPosition.x + recycleOffset < player.DistanceTraveled )
			Recycle();
	}
	
	void Recycle() {
		
		nextPosition.x = player.DistanceTraveled + Random.Range(40,60);
		
		lastCoinPosition = CoinDistribution.RandomDistribution( coinQueue, nextPosition );
	}
	
	void GameStart () {
		
		nextPosition 		= startingPosition;
		lastCoinPosition	= Vector3.zero;
		
		enabled = true;
		
		Recycle();
	}
	
	void GameOver () {
		enabled = false;
	}
}

