using UnityEngine;
using System.Collections;

public class ScrollText : MonoBehaviour
{
	public float charactersPerSecond = 8.0f;
//	public string text = "Here is some text to scroll";
	
	Rect rect;

	string textUsing;
	string scrollBasis;
	string scrollText;

	int currChar = 0;
	float timer = 0.0f;
	float secondsPerCharacter;
	 
	SessionData session;
	LoadedData loaded;
	
	string text = "Total Coins : 100 " +
		"|| Max Altitude : 50 mtrs " +
		"|| Max Distance : 3000 mtrs " +
		"|| Bigger Size : 10 " +
		"|| Jumps : 50 " +
		"|| Capsules Eated : 5 " +
		"|| Enemies Collided : 3 " +
		"|| Enemies Passed : 50 " +
		"|| Missiles Collided: 50 " +
		"|| Missiles Passed : 50";
	
	bool show;
	
	void Start () {
		
		GameEventManager.GameInit 	+= GameInit;
		GameEventManager.GameStart 	+= GameStart;
		GameEventManager.GameOver 	+= GameOver; 
		
		rect = new Rect( 0, Screen.height - 25, Screen.width + 50, 20 );
		
	    NewText();
		
		enabled = false;
	}
	 
	void Update () {
		
		SetStatisticsText();
		
	    if (textUsing != text)
	       NewText();
	 
	    secondsPerCharacter = 1.0f / charactersPerSecond;
	    
		if ( timer > secondsPerCharacter) {
	      int iT  = Mathf.FloorToInt(timer / secondsPerCharacter);
	      currChar = (currChar + iT) % textUsing.Length;
	      timer -= iT * secondsPerCharacter;
	      scrollText = scrollBasis.Substring(currChar, textUsing.Length);  
	    }
		
		timer += Time.deltaTime;
	}
	 
	void OnGUI() {
		GUI.Label(rect, scrollText);
	}
	 
	void NewText() {
	    textUsing = text;
	    scrollBasis = textUsing+textUsing;
	    currChar = 0;
	    scrollText = scrollBasis.Substring(currChar, textUsing.Length);
	    timer = 0.0f;
	}
	
	void GameInit() {
		
		if( show )
			enabled = true;
		
		PlayerDataManager dataManager = GameObject.FindGameObjectWithTag( "GameData" ).GetComponent<PlayerDataManager>();
		
		session = dataManager.Session;
		loaded	= dataManager.Loaded;
	}
	
	void GameStart() {
		
		enabled = false;
	}
	
	void GameOver() {
		
		show = true;
	}
	
	void SetStatisticsText() {
		
		text = "" +
			"coins : " 		+ session.coins + " : " + loaded.max_coins + " : " + loaded.total_coins + " || " +
			"capsuels : " 	+ session.capsules + " : " + loaded.max_capsules + " : " + loaded.total_capsules + " || " +
			"distance : " 	+ session.distance + " : " + loaded.max_distance + " : " + loaded.total_distance + " || " +
			"altitude : " 	+ session.altitude + " : " + loaded.max_altitude + " || " +
			"size : " 		+ session.size + " : " + loaded.max_size + " || " +
			"velocity : " 	+ session.velocity + " : " + loaded.max_velocity + " || " +
			"enemies : " 	+ session.enemies + " : " + " : " + loaded.total_enemies + " || " +
			"enemies collided : " + session.enemies_collided + " : " + loaded.total_enemies_collided + " || " +
			"missiles : " 	+ session.missiles + " : " + loaded.total_missiles + " || " +
			"missiles collided : " + session.missiles_collided + " : " + loaded.total_missiles_collided + " || ";
	}
}

