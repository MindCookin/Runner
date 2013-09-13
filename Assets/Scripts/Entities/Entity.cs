using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	protected Vector3 mNextPosition = Vector3.zero;
	
	void Start()
	{
		// listen to game events
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		renderer.enabled	= false;
		enabled 			= false;
	}
	
	virtual protected void GameStart() {
		// enable to play
		renderer.enabled	= true;
		enabled 			= true;
	}
	
	virtual protected void GameOver() {
		// disable on gameover
		renderer.enabled	= false;
		enabled 			= false;
	}
	
	virtual public void Place( Vector3 nextPosition ) {
		throw new UnityException( "You should overwrite this method" );
	}
	
	public Vector3 NextPosition{ get { return mNextPosition; } }
}

