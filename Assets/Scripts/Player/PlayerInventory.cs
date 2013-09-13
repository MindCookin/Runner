using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
	public Vector3 	boostVelocity;
	
	private int 	_boosts;

	// Use this for initialization
	void Start ()
	{
		// listen to game events
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		// initialize GUI texts 
		GUIManager.SetBoosts(_boosts);
	}
	
	private void OnTriggerEnter ( Collider col ) {
		
		switch( col.gameObject.tag ) 
		{
			case "Coin":
				addCoin();
				col.gameObject.SetActive(false);
			break;
			case "Cannon":
				col.GetComponent<CannonPicker>().Fill();
			break;
			case "Pickup":
				col.gameObject.SetActive(false);
				addBoost();
			break;
		} 
	}
	
	void GameStart() {
		
		_boosts = 0;
	}
	
	void GameOver() {
		
	}
	
	void addBoost() {
		
		_boosts += 1;
		
		GUIManager.SetBoosts(_boosts);
	}
	
	void addCoin() {
		GUIManager.AddCoin();
	}
}

