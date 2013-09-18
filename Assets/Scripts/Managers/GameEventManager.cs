
public static class GameEventManager {

	public delegate void GameEvent();
	
	public static event GameEvent GameStart, GameOver, GameInit;

	public static void TriggerGameStart(){
		if(GameStart != null){
			GameStart();
		}
	}

	public static void TriggerGameOver(){
		if(GameOver != null){
			GameOver();
		}
	}
	
	public static void TriggerGameInit(){
		if(GameInit != null){
			GameInit();
		}
	}
}
