using UnityEngine;
using System.Collections;

public class DropItemsManager : MonoBehaviour {
	
	public int distanceFromPlayer;
	public Vector3 		recycleOffset;
	public Transform 	dropItem;
	
	private Vector3 nextPosition;
	private PlayerMove player;
	
	void Start () {
		
		GameEventManager.GameInit 	+= GameInit;
		GameEventManager.GameStart 	+= GameStart;
		GameEventManager.GameOver 	+= GameOver;	
		
		player 		= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		
		dropItem = (Transform) Instantiate( dropItem );
		dropItem.transform.parent = transform;
		dropItem.gameObject.SetActive(false);
		
		enabled = false;
	}
	
	void Update () {
		
		if ( nextPosition.x + recycleOffset.x < player.DistanceTraveled )
			Recycle();
	}
	
	void Recycle() {
		
		if( Random.value < LevelStateManager.GetInstance().DropPickerPercent )
		{
			Debug.Log("Drop Item");
			
			dropItem.gameObject.SetActive(true);
			
			nextPosition = player.transform.localPosition;
			nextPosition.x += distanceFromPlayer;
			nextPosition.y += 15;
			
			dropItem.position = nextPosition;
			dropItem.GetComponent<PickupItem>().Reset();
		}
	}
	
	void GameStart () {
		
		Recycle();
		
		enabled = true;
	}
	
	void GameInit () {
	}
	
	void GameOver () {
		
		dropItem.gameObject.SetActive(false);
		
		enabled = false;
	}
}
