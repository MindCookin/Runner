using UnityEngine;
using System.Collections;

public class PlayerExplosion : MonoBehaviour {
	
	public float timeToGameOver;
	
	ParticleSystem particles;
	
	// Use this for initialization
	void Awake () {
		particles = GetComponent<ParticleSystem>();
	}
	
	public void Explode() {
		
		particles.Play();
		
		Invoke("TriggerGameOver", timeToGameOver );
	}
	
	void TriggerGameOver(){
		GameEventManager.TriggerGameOver();	
	}
}
