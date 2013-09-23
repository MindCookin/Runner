using UnityEngine;
using System.Collections;

public class PlayerExplosion : MonoBehaviour {
	
	public float timeToGameOver;
	
	ParticleSystem particles;
	PlayerShoot playerShoot;
	PlayerSounds playerSounds;
	
	// Use this for initialization
	void Awake () {
		
		playerSounds= GetComponent<PlayerSounds>();
		playerShoot = GetComponent<PlayerShoot>();
		
		particles = GetComponent<ParticleSystem>();
	}
	
	public void Explode() {
		
		playerSounds.PlaySounds( playerSounds.explosion );
		
		playerShoot.StopShooting();
		
		particles.Play();
		renderer.enabled = false;
		rigidbody.Sleep();
		
		Invoke("TriggerGameOver", timeToGameOver );
	}
	
	void TriggerGameOver(){
		
		rigidbody.WakeUp();
		GameEventManager.TriggerGameOver();	
	}
}
