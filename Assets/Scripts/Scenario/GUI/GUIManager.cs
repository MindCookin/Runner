using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	public GUIText instructionsText, gameplayStatsText;
	
	private static GUIManager instance;
	public int coins, distance;
	
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
		instance.UpdateGameplayText();
	}

	public static void SetDistance(float dist){
		
		if ( instance )
		{
			instance.distance = Mathf.FloorToInt( dist );
			instance.UpdateGameplayText();
		}
	}

	void Update () {
		
		if(Input.GetButtonDown("Jump") || Input.touches.Length > 0 )
			GameEventManager.TriggerGameStart();
	}
	
	private void GameStart () {
	
		instructionsText.enabled = false;
		gameplayStatsText.enabled= true;
		
		enabled = false;
	}
	
	private void GameInit () {
	
		coins = 0;
		instructionsText.enabled = true;
		gameplayStatsText.enabled= false;
		
		enabled = true;
	}
	
	private void GameOver () {
		
		gameplayStatsText.enabled= false;
		
		PlayerDataManager.SetValue( SessionData.COINS, coins );
	}
	
	public void UpdateGameplayText() {
		gameplayStatsText.text = "DISTANCE : " + distance + " || COINS : " + coins;	
	}
}