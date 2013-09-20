using UnityEngine;
using System.Collections;

public class PlayerExplosion : MonoBehaviour {
	
	public float timeToGameOver;
	
	ParticleSystem particles;
	PlayerShoot playerShoot;
	
	// Use this for initialization
	void Awake () {
		playerShoot = GetComponent<PlayerShoot>();
		
		particles = GetComponent<ParticleSystem>();
	}
	
	public void Explode() {
		
		playerShoot.StopShooting();
		
		particles.Play();
		renderer.enabled = false;
		
		Invoke("TriggerGameOver", timeToGameOver );
	}
	
	void TriggerGameOver(){
		
		GameEventManager.TriggerGameOver();	
	}
}
