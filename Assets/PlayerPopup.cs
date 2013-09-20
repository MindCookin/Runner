using UnityEngine;
using System.Collections;

public class PlayerPopup : MonoBehaviour {
	
	public int width, height;
	
	string statisticsText = "Total Coins : 100" +
		"\nMax Altitude : 50 mtrs" +
		"\nMax Distance : 3000 mtrs" +
		"\nBigger Size : 10" +
		"\nJumps : 50" +
		"\nCapsules Eated : 5" +
		"\nEnemies Collided : 3" +
		"\nEnemies Passed : 50" +
		"\nMissiles Collided: 50" +
		"\nMissiles Passed : 50";
	
	string highscoresText = "HIGHLIGHTS";
	
	bool pressedStatistics, pressedHighscores;
	
	void Awake() {
		
		GameEventManager.GameReset 	+= GameReset;
		GameEventManager.GameInit 	+= GameInit;
		GameEventManager.GameStart 	+= GameStart;
		GameEventManager.GameOver 	+= GameOver;
		
//		enabled = false;
	}
	
	void OnGUI(){
		
		
		if( pressedStatistics )
		{
			GUI.Box (new Rect ( Screen.width / 2 - width/2, Screen.height / 2 - height/2, width, height ), statisticsText );
			pressedHighscores = false;
			
			if ( GUI.Button ( new Rect (Screen.width - 130, 20, 120, 20), "Hide Stats" ) ) 
				pressedStatistics = false;
			
		} else {
			
			if ( GUI.Button ( new Rect (Screen.width - 130, 20, 120, 20), "Show Stats" ) ) 
				pressedStatistics = true;
		}
		
		
		if( pressedHighscores )
		{
			GUI.Box (new Rect ( Screen.width / 2 - width/2, Screen.height / 2 - height/2, width, height ), highscoresText );
			pressedStatistics = false;
			
			if ( GUI.Button ( new Rect (Screen.width - 130, 45, 120, 20), "Hide Credits" ) ) 
				pressedHighscores = false;
			
		} else {
			
			if ( GUI.Button ( new Rect (Screen.width - 130, 45, 120, 20), "Show Credits" ) ) 
				pressedHighscores = true;
		}
			
		if ( GUI.Button ( new Rect (Screen.width / 2 - 140, Screen.height - 80, 130, 20), "Share on Facebook" ) ) 
			FacebookShare();
			
		if ( GUI.Button ( new Rect (Screen.width / 2, Screen.height - 80, 120, 20), "Share on Twitter" ) ) 
			TwitterShare();
		
	}
	
	void GameInit() {
		enabled = true;
	}
	
	void GameReset() {
		enabled = true;
	}
	
	void GameStart() {
		
		pressedHighscores = false;
		pressedStatistics = false;
		
		enabled = false;
	}
	
	void GameOver() {
	}
	
	void FacebookShare() {
		Debug.Log("Share On Faceboook!");
	}
	
	void TwitterShare() {
		Debug.Log("Share On Twitter!");
		
	}
}
