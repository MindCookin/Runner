using UnityEngine;
using System.Collections;

public class GooglePlusSocial : MonoBehaviour
{
	public static string LEADERBOARD_COINS 		= "CgkIxo6k0egXEAIQCA"; //
	public static string LEADERBOARD_DISTANCE	= "CgkIxo6k0egXEAIQCQ";
	
	public static string ACHIEVEMENT_BIGFELLA 	= "CgkIxo6k0egXEAIQAQ";	//
	public static string ACHIEVEMENT_50COINS 	= "CgkIxo6k0egXEAIQAg";	//
	public static string ACHIEVEMENT_100COINS 	= "CgkIxo6k0egXEAIQAw";	//
	public static string ACHIEVEMENT_200COINS 	= "CgkIxo6k0egXEAIQBA";	//
	public static string ACHIEVEMENT_SNIPER 	= "CgkIxo6k0egXEAIQBQ";
	public static string ACHIEVEMENT_PINATA 	= "CgkIxo6k0egXEAIQBg"; //
	public static string ACHIEVEMENT_HOOKED 	= "CgkIxo6k0egXEAIQBw"; //

	public static void SubmitScore( string leaderboard, int score )
	{
		if( Social.localUser.authenticated )
			Social.ReportScore( score, leaderboard, OnSubmitScore);
	}
	
	private static void OnSubmitScore( bool result )
    {
        Debug.Log("GPGUI: OnSubmitScore: " + result);
    }
	
	public static void SubmitAchievement( string achievement )
	{
		if( Social.localUser.authenticated )
        	Social.ReportProgress( achievement, 100.0, OnUnlockAC);	
	}
	
    private static void OnUnlockAC( bool result )
    {
        Debug.Log("GPGUI: OnUnlockAC " + result);
    }
}

