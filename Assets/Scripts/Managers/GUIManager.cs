using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	public GUIText boostsText, distanceText, gameOverText, instructionsText, runnerText, coinsText;
	private static GUIManager instance;
	public int coins;
	
	void Start () {
		// create instance to enable boots count
		instance = this;
		
		// listeners
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		// disable by default
		gameOverText.enabled = false;
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
	
		coins = 0;
		coinsText.text = "No Coins";
		
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		
		enabled = false;
	}
	
	private void GameOver () {
		
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		
		enabled = true;
	}
}