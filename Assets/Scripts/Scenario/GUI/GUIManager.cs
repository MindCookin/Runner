using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	public GUIText instructionsText, gameplayStatsText;
	
	private static GUIManager instance;
	public int coins, distance;
	
	int startingTimes = 0;
	
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
		
		if ( instance.coins == 50 )
			GooglePlusSocial.SubmitAchievement( GooglePlusSocial.ACHIEVEMENT_50COINS );
		else if ( instance.coins == 100 )
			GooglePlusSocial.SubmitAchievement( GooglePlusSocial.ACHIEVEMENT_100COINS );
		else if ( instance.coins == 200 )
			GooglePlusSocial.SubmitAchievement( GooglePlusSocial.ACHIEVEMENT_200COINS );
	}

	public static void SetDistance(float dist){
		
		if ( instance )
		{
			instance.distance = Mathf.FloorToInt( dist );
			instance.UpdateGameplayText();
		}
	}

	void Update () {
		
		if(Input.GetButtonDown("Jump") )
			GameEventManager.TriggerGameStart();
		
		if( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended )
		{
			if ( !GooglePlusButton.PRESSED )
				GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
	
		instructionsText.enabled = false;
		gameplayStatsText.enabled= true;
		
		enabled = false;
		
		startingTimes++;
		
		if (startingTimes >= 10 )
			GooglePlusSocial.SubmitAchievement( GooglePlusSocial.ACHIEVEMENT_HOOKED );
	}
	
	private void GameInit () {
	
		coins = 0;
		instructionsText.enabled = true;
		gameplayStatsText.enabled= false;
		
		enabled = true;
	}
	
	private void GameOver () {
		
		gameplayStatsText.enabled= false;
		
		GooglePlusSocial.SubmitScore( GooglePlusSocial.LEADERBOARD_COINS, coins );
		GooglePlusSocial.SubmitScore( GooglePlusSocial.LEADERBOARD_DISTANCE, distance );
		
		PlayerDataManager.SetValue( SessionData.COINS, coins );
	}
	
	public void UpdateGameplayText() {
		gameplayStatsText.text = "DISTANCE : " + distance + " || COINS : " + coins;	
	}
}