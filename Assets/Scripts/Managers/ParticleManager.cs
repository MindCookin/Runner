using UnityEngine;

public class ParticleManager : MonoBehaviour {

	public ParticleSystem[] particleSystems;

	void Start () {
		
		// add listeners
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		// disable onload
		GameOver();
	}

	private void GameStart () {
		
		// enable on game start
		for(int i = 0; i < particleSystems.Length; i++){
			particleSystems[i].Clear();
			particleSystems[i].enableEmission = true;
		}
	}

	private void GameOver () {
		
		// enable on game over
		for(int i = 0; i < particleSystems.Length; i++){
			particleSystems[i].enableEmission = false;
		}
	}
}