using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	public GUIText boostsText, distanceText, gameOverText, instructionsText, coinsText;
	
	private static GUIManager instance;
	public int coins;
	
	void Start () {
		// create instance to enable boots count
		instance = this;
		
		// listeners
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameInit += GameInit;
		
		// disable by default
		gameOverText.enabled = false;
		
		
		GameEventManager.TriggerGameInit();
	}
	
	public static void AddCoin() {
		
		instance.coins++;
		
		if( instance )
			instance.coinsText.text = "Coins : " + instance.coins.ToString();
	}

	public static void SetBoosts(int boosts){
		if( instance )
			instance.boostsText.text = "Boosts : " + boosts.ToString();
	}

	public static void SetDistance(float distance){
		if( instance )
			instance.distanceText.text = distance.ToString("f0");
	}

	void Update () {
		
		if(Input.GetButtonDown("Jump"))
			GameEventManager.TriggerGameStart();
	}
	
	private void GameStart () {
	
		distanceText.enabled = true;
		boostsText.enabled = true;
		coinsText.enabled = true;
		
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		
		enabled = false;
	}
	
	private void GameInit () {
	
		coins = 0;
		coinsText.text = "No Coins";
		
		distanceText.enabled = false;
		boostsText.enabled = false;
		coinsText.enabled = false;
		instructionsText.enabled = true;
		gameOverText.enabled = false;
		
		enabled = true;
	}
	
	private void GameOver () {
		
		gameOverText.enabled = true;
		
		Invoke("TriggerInit", 2 );
	}
	
	private void TriggerInit() {
		GameEventManager.TriggerGameInit();	
	}
}