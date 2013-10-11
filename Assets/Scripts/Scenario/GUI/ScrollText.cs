using UnityEngine;
using System.Collections;

public class ScrollText : MonoBehaviour
{
	public float charactersPerSecond = 8.0f, divider;
	public GUIText textField;
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
		
		textField.enabled = false;
		enabled = false;
	}
	 
	void Update () {
		
		SetStatisticsText();
		
	    if (textUsing != text)
	       NewText();
	 
	    secondsPerCharacter = divider / charactersPerSecond;
	    
		if ( timer > secondsPerCharacter) {
	      int iT  = Mathf.FloorToInt(timer / secondsPerCharacter);
	      currChar = (currChar + iT) % textUsing.Length;
	      timer -= iT * secondsPerCharacter;
	      scrollText = scrollBasis.Substring(currChar, textUsing.Length);  
	    }
		
		timer += Time.deltaTime;
		
		textField.text = scrollText;
	}
	/* 
	void OnGUI() {
		GUI.Label(rect, scrollText);
	}*/
	 
	void NewText() {
	    textUsing = text;
	    scrollBasis = textUsing+textUsing;
	    currChar = 0;
	    scrollText = scrollBasis.Substring(currChar, textUsing.Length);
	    timer = 0.0f;
	}
	
	void GameInit() {
		
		if( show )
		{
			enabled = true;
			textField.enabled = true;
		}
		
		PlayerDataManager dataManager = GameObject.FindGameObjectWithTag( "GameData" ).GetComponent<PlayerDataManager>();
		
		session = dataManager.Session;
		loaded	= dataManager.Loaded;
	}
	
	void GameStart() {
		
		enabled = false;
		textField.enabled = false;
	}
	
	void GameOver() {
		
		show = true;
	}
	
	void SetStatisticsText() {
		
		text = "" +
			"COINS CAPTURED: " + session.coins + " || " + 
			"MAX COINS CAPTURED: " + loaded.max_coins + " || " +
			"TOTAL COINS CAPTURED: " + loaded.total_coins + " || " +
			"CAPSULES EATED: " 	+ session.capsules + " || " +
			"MAX CAPSULES EATED: " + loaded.max_capsules + " || " +
			"TOTAL CAPSULES EATED: " + loaded.total_capsules + " || " +
			"DISTANCE TRAVELED: " 	+ session.distance + " || " + 
			"MAX DISTANCE TRAVELED: " + loaded.max_distance + " || " +
			"TOTAL DISTANCE TRAVELED: " + loaded.total_distance + " || " +
			"ALTITUDE REACHED: " 	+ session.altitude + " || " +
			"MAX ALTITUDE REACHED: " + loaded.max_altitude + " || " +
			"MAX SIZE ON THIS GAME: " + session.size + " || " +
			"MAX SIZE EVER " + loaded.max_size + " || " +
			"MAX VELOCITY ON THIS GAME: " 	+ session.velocity + " || " +
			"MAX VELOCITY EVER: " + loaded.max_velocity + " || " +
			"ENEMIES PASSED: " 	+ session.enemies + " || " +
			"TOTAL ENEMIES PASSED: " + loaded.total_enemies + " || " +
			"ENEMIES COLLIDED: " + session.enemies_collided +" || " +
			"ENEMIES SHOOTED: " + session.enemies_shooted +" || " +
			"TOTAL ENEMIES COLLIDED: " + loaded.total_enemies_collided + " || " +
			"MISSILES PASSED: " 	+ session.missiles + " || " +
			"TOTAL MISSILES PASSED: " + loaded.total_missiles + " || " +
			"MISSILES COLLIDED: " + session.missiles_collided +" || " + 
			"TOTAL MISSILES COLLIDED: " + loaded.total_missiles_collided + " || ";
	}
}

