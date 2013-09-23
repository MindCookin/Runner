using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
	
	public float crossFadeSpeed, introVolume, gameVolume;
	public AudioSource intro, game;
	
	bool initialize, started;
	
	void Start () {
		
		GameEventManager.GameInit += GameInit;
		GameEventManager.GameStart += GameStart;
		
		intro.volume = introVolume;
	}
	
	void GameInit() {
		
		intro.Stop();
		intro.Play();
		
		enabled = true;
		
		initialize 	= true;
		started 	= false;
	}
	
	void GameStart() {
		
		game.Stop();
		game.Play();
		
		enabled = true;
		
		initialize 	= false;
		started 	= true;
	}
	
	void Update() {
		
		if ( initialize )
		{
			if ( intro.volume == introVolume && game.volume == 0 )
				enabled = false;
			
			intro.volume = Mathf.Lerp( intro.volume, introVolume, Time.deltaTime * crossFadeSpeed );
			game.volume = Mathf.Lerp( game.volume, 0, Time.deltaTime * crossFadeSpeed );	
		
		} else if ( started ) {
			
			if ( intro.volume == 0 && game.volume == gameVolume )
				enabled = false;
			
			intro.volume = Mathf.Lerp( intro.volume, 0, Time.deltaTime * crossFadeSpeed );
			game.volume = Mathf.Lerp( game.volume, gameVolume, Time.deltaTime * crossFadeSpeed );
		}
	}
}
