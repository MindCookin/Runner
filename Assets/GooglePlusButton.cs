using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.SocialPlatforms;

public class GooglePlusButton : MonoBehaviour {
	
	public static bool PRESSED = false;
	
	public GUITexture googlePlusTexture;
	public GUIText googlePlusText;
	
	Rect buttonsRect = new Rect( 0, 0, 160, 40 );
	
	enum GPLoginState {loggedout, loggedin};
	GPLoginState m_loginState = GPLoginState.loggedout;
	
	Touch touchObj;
	
	// Use this for initialization
	void Start () {
		
        Social.Active = new UnityEngine.SocialPlatforms.GPGSocial();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameInit += GameInit;
		
		googlePlusText.text = "Connect with Google Plus\nfor leaderboards and achievements.";
	}
	
	void OnApplicationFocus (bool focus) {
		
		if( focus )
			PRESSED = false;	
	}
	
	void OnGUI () {
		
		if( !PRESSED )
		{	
			if( Input.touchCount > 0 )
				touchObj = Input.GetTouch(0);
	
			if( m_loginState == GPLoginState.loggedout )
			{
				if (touchObj.phase == TouchPhase.Began) 
				{
				    if( googlePlusTexture.GetScreenRect().Contains( touchObj.position) )
				    {
						PRESSED = true;
		        		Social.localUser.Authenticate(OnAuthCB);
				    }
				}
				
			} else {
				
				googlePlusTexture.enabled = googlePlusText.enabled = false;
				
				buttonsRect.x = Screen.width/2 - buttonsRect.width - 5;
				buttonsRect.y = Screen.height - Screen.height/5 - buttonsRect.height/2 + 10;
				
				if( GUI.Button( buttonsRect, "Show Achievements" ) )
				{
					PRESSED = true;
                	Social.ShowAchievementsUI();
				}
				
				buttonsRect.x += buttonsRect.width + 10;
				
				if( GUI.Button( buttonsRect, "Show LeaderBoards" ) )
				{
					PRESSED = true;
					Social.ShowLeaderboardUI();
				}
			}
		}
	}
	
	void GameInit(){
		gameObject.SetActive( true );
	}
	
	void GameStart(){
		gameObject.SetActive( false );
	}
	
	// CALLBACKS

    void OnAuthCB(bool result)
    {
        Debug.Log("GPGUI: Got Login Response: " + result);
		
		// set login State
		GPGAuthResult( result );
		
		if( result )
		{
			// load Achievements
        	Social.LoadAchievements(OnLoadAC);
		} else {
			// if we are not logged, let the user continue playing
			PRESSED = false;	
		}
    }

    public void OnLoadAC(IAchievement[] achievements)
    {
        Debug.Log("GPGUI: Loaded Achievements: " + achievements.Length);
		
		PRESSED = false;
    }
	
	public void GPGAuthResult(bool result)
	{
		// success/failed
		if(result) {
			m_loginState = GPLoginState.loggedin;
		} else 
			m_loginState = GPLoginState.loggedout;
	}
}
