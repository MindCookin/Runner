using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
	public Vector3 	boostVelocity;
	
	private PlayerSize playerSize;
	private PlayerShoot playerShoot;
	private PlayerForceField playerForceField;
	private PlayerColors playerColors;
	
	// Use this for initialization
	void Start()
	{
		playerSize 			= GetComponent<PlayerSize>();
		playerShoot			= GetComponent<PlayerShoot>();
		playerForceField	= GetComponent<PlayerForceField>();
		playerColors		= GetComponent<PlayerColors>();
		
		// listen to game events
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
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
			
				string pickType = col.renderer.material.name.Split('_')[1];
				Picked( pickType );	
			
			break;
		} 
	}
	
	void GameStart() {
		
	}
	
	void GameOver() {
		
	}
	
	void addCoin() {
		GUIManager.AddCoin();
	}
	
	void Picked( string type ) {
		
		switch( type )
		{
			case "Red" : 	playerColors.ChangeColor( PlayerColors.RED ); 	break;
			case "Green" :  playerColors.ChangeColor( PlayerColors.GREEN ); break;
			case "Blue" :  	playerColors.ChangeColor( PlayerColors.BLUE ); 	break;
			case "Yellow" : playerColors.ChangeColor( PlayerColors.YELLOW );break;
			case "Big" :  	playerSize.ChangeSize( PlayerSize.BIG ); 		break;
			case "Small" :  playerSize.ChangeSize( PlayerSize.SMALL ); 		break;
			case "Shoot" :  playerShoot.StartShooting(); 					break;
			case "Force" :  playerForceField.Show(); 						break;	
		}
		
		Debug.Log("PICKUP" + " " + type );
	}
}

