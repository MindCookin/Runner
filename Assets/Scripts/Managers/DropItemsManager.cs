using UnityEngine;
using System.Collections;

public class DropItemsManager : MonoBehaviour {
	
	public Vector3 		recycleOffset;
	public Transform 	dropItem;
	
	private Vector3 nextPosition;
	private PlayerMove player;
	
	LevelStateManager level;
	
	void Start () {
		
		level = LevelStateManager.instance;
	
		GameEventManager.GameInit 	+= GameInit;
		GameEventManager.GameStart 	+= GameStart;
		GameEventManager.GameOver 	+= GameOver;	
		
		player 		= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		
		dropItem = (Transform) Instantiate( dropItem );
		dropItem.transform.parent = transform;
		
		enabled = false;
	}
	
	void Update () {
		
		if ( nextPosition.x + recycleOffset.x < player.DistanceTraveled )
			Recycle();
	}
	
	void Recycle() {
		
		if( Random.value < level.DropPickerPercent )
		{
			nextPosition = player.transform.localPosition;
			nextPosition.x += 20;
			nextPosition.y += 15;
			
			dropItem.position = nextPosition;
			dropItem.GetComponent<PickupItem>().Reset();
		}
	}
	
	void GameStart () {
		
		enabled = true;
	}
	
	void GameInit () {
	
		Recycle();
	}
	
	void GameOver () {
		enabled = false;
	}
}
