using UnityEngine;
using System.Collections;

public class EnemyJumperExplode : MonoBehaviour {
	
	GameObject player;
	ParticleSystem particles;
	
	public Vector3 force, torque;
	
	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		
		particles = transform.FindChild("JumperParticles").GetComponent<ParticleSystem>();
	}
	
	void OnCollisionEnter( Collision collision ){
		
		if( renderer.enabled && collision.gameObject.tag == "Player" )
			Explode();
	}
	
	public void Explode()
	{ 	
		// explode enemy
		particles.Play();
		renderer.enabled = false;
		rigidbody.Sleep();
		rigidbody.detectCollisions = false;
		
		// explode player
		player.rigidbody.angularVelocity = Vector3.zero;
		player.rigidbody.velocity = Vector3.zero;
		player.rigidbody.sleepVelocity = 0;
		
		player.rigidbody.AddForce( force, ForceMode.Impulse );
		player.rigidbody.AddTorque( torque, ForceMode.Impulse );
	}
}
