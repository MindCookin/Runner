using UnityEngine;
using System.Collections;

public class EnemyJumperMove : MonoBehaviour {
	
	public Vector3 jumpingForce;
	public int Ylimit;
	
	private bool _touchingPlatform;
	
	public void Setup()
	{
		renderer.enabled = true;
		rigidbody.detectCollisions = true;
		
		transform.Translate( 0, 2, 0 );
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.velocity = Vector3.zero;
		rigidbody.sleepVelocity = 0;
		rigidbody.WakeUp();
	}
	
	void Update () {
		
		if ( _touchingPlatform )
		{
			rigidbody.AddForce( jumpingForce, ForceMode.Impulse );
			_touchingPlatform = false;
		}
		
		if( transform.position.y < Ylimit )
			gameObject.SetActive( false );
	}
	
	void OnCollisionEnter ( Collision collision ) {
		
		if( collision.gameObject.tag == "Platform" )	// check platform collision
			_touchingPlatform = true;
	}

	void OnCollisionExit ( Collision collision ) {
		
		if( collision.gameObject.tag == "Platform" )	// check platform collision
			_touchingPlatform = false; 
	}
}
