using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	public GUIText instructionsText;
	
	private static GUIManager instance;
	public int coins;
	
	void Start () {
		
		if ( SystemInfo.deviceType == DeviceType.Handheld )
			instructionsText.text = "Touch the screen to Jump. Touch it again for Double Jump.";
		else 
			instructionsText.text = "Press Jump (x or space) to play. Press it twice for Double Jump.";
		
		// create instance to enable boots count
		instance = this;
		
		// listeners
		GameEventManager.GameStart 	+= GameStart;
		GameEventManager.GameOver 	+= GameOver;
		GameEventManager.GameInit 	+= GameInit;
		
		GameEventManager.TriggerGameInit();
	}
	
	public static void AddCoin() {
		
		instance.coins++;
	}

	public static void SetDistance(float distance){
	}

	void Update () {
		
		if(Input.GetButtonDown("Jump") || Input.touches.Length > 0 )
			GameEventManager.TriggerGameStart();
	}
	
	private void GameStart () {
	
		instructionsText.enabled = false;
		
		enabled = false;
	}
	
	private void GameInit () {
	
		coins = 0;
		instructionsText.enabled = true;
		
		enabled = true;
	}
	
	private void GameOver () {
		
		PlayerDataManager.SetValue( SessionData.COINS, coins );
	}
}