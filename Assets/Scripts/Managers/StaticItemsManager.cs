using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaticItemsManager : MonoBehaviour
{
	public Vector3 startingPosition;
	public int recycleOffset;
	public Transform coin, cannon, pickup;
	public float cannonPercent, pickupPercent;
	public Vector3 cannonPosition, pickupPosition;
	
	private Vector3 nextPosition, lastItemPosition;
	private Queue<Transform> coinQueue;
	private int queuedQuantity = 10;
	private Transform cannonInstance, pickupInstance;
		
	private PlayerMove player;
	
	void Awake() {
		
		nextPosition = startingPosition;
	
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;	
	
		cannonInstance = (Transform)Instantiate(cannon);
		pickupInstance = (Transform)Instantiate(pickup);
		
		cannonInstance.parent = transform;
		pickupInstance.parent = transform;
		
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
		if ( lastItemPosition.x + recycleOffset < player.DistanceTraveled )
		{
			nextPosition.x = player.DistanceTraveled + 30;
			Recycle();
		}
	}
	
	void Recycle() {
		
		lastItemPosition = CoinDistribution.RandomDistribution( coinQueue, nextPosition );
		
		PlaceInBetweenItems();
	}
	
	void PlaceInBetweenItems() {
		
		bool placeCannon = Random.value < cannonPercent;
		bool placePickup = Random.value < pickupPercent;
		
		if( placeCannon && placePickup )
		{
			if( Random.value < .5f )
			{
				RecycleItem( cannonInstance );
				RecycleItem( pickupInstance );
			} else {
				RecycleItem( pickupInstance );
				RecycleItem( cannonInstance );
			}
			
		} else if ( placeCannon ) {
				RecycleItem( cannonInstance );
		} else if ( placePickup ) {
				RecycleItem( pickupInstance );
		}	
	}
	
	void RecycleItem( Transform item ) {
		
		Vector3 itemPosition = ( item.tag == "Cannon" ) ? cannonPosition : pickupPosition;
		itemPosition.x = lastItemPosition.x + Random.Range( 20, 30 );
		
		item.GetComponent<SimpleItem>().Reset();
		item.position = itemPosition;
		
		lastItemPosition.x = itemPosition.x;
	}
	
	void GameStart () {
		
		nextPosition 		= startingPosition;
		lastItemPosition	= Vector3.zero;
		
		enabled = true;
		
		Recycle();
	}
	
	void GameOver () {
		enabled = false;
	}
}