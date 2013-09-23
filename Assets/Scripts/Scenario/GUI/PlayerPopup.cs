using UnityEngine;
using System.Collections;

public class PlayerPopup : MonoBehaviour {
	
	string statisticsText = "Total Coins : 100 " +
		"|| Max Altitude : 50 mtrs " +
		"|| Max Distance : 3000 mtrs " +
		"|| Bigger Size : 10 " +
		"|| Jumps : 50 " +
		"|| Capsules Eated : 5 " +
		"|| Enemies Collided : 3 " +
		"|| Enemies Passed : 50 " +
		"|| Missiles Collided: 50 " +
		"|| Missiles Passed : 50";
	
	bool showStatistics;
	
	SessionData session;
	LoadedData loaded;
	Rect labelRect1, labelRect2;
	
	void Awake() {
		
		GameEventManager.GameInit 	+= GameInit;
		GameEventManager.GameStart 	+= GameStart;
		GameEventManager.GameOver 	+= GameOver;
		
		labelRect1 = new Rect( 0, 10, Screen.width, 20 );
		labelRect2 = new Rect( 0, 10, Screen.width, 20 );
	}
	
	void OnGUI(){
		
		
		if( showStatistics )
		{
		//	SetStatisticsText();
/*			labelRect1.x -= 1;
			labelRect2.x = labelRect1.x + labelRect1.width;
			GUI.Label( labelRect1, statisticsText );
			GUI.Label( labelRect2, statisticsText );
		*/}
	}
	
	void GameInit() {
		enabled = true;
		
		PlayerDataManager dataManager = GameObject.FindGameObjectWithTag( "GameData" ).GetComponent<PlayerDataManager>();
		session = dataManager.Session;
		loaded	= dataManager.Loaded;
		
		showStatistics = true;
	}
	
	void GameStart() {
		
		showStatistics = false;
		
		enabled = false;
	}
	
	void GameOver() {
		showStatistics = true;
	}
	
	void SetStatisticsText() {
		statisticsText = "" +
			"coins : " 		+ session.coins + " : " + loaded.max_coins + " : " + loaded.total_coins + "\n" +
			"capsuels : " 	+ session.capsules + " : " + loaded.max_capsules + " : " + loaded.total_capsules + "\n" +
			"distance : " 	+ session.distance + " : " + loaded.max_distance + " : " + loaded.total_distance + "\n" +
			"altitude : " 	+ session.altitude + " : " + loaded.max_altitude + "\n" +
			"size : " 		+ session.size + " : " + loaded.max_size + "\n" +
			"velocity : " 	+ session.velocity + " : " + loaded.max_velocity + "\n" +
			"enemies : " 	+ session.enemies + " : " + " : " + loaded.total_enemies + "\n" +
			"enemies collided : " + session.enemies_collided + " : " + loaded.total_enemies_collided + "\n" +
			"missiles : " 	+ session.missiles + " : " + loaded.total_missiles + "\n" +
			"missiles collided : " + session.missiles_collided + " : " + loaded.total_missiles_collided;
	}
}
