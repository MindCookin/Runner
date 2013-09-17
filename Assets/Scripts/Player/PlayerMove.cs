using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	
	public float 	acceleration;
	public Vector3 	jumpVelocity;
	public float 	gameOverY;
	public bool		autoMove;
	
	private Vector3 _startPosition;
	private float 	_distanceTraveled;
	private bool 	_touchingPlatform, _onDoubleJump;
		
	private Vector3 constantMove;
	
	void Start() {
	
		// listen to game events
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		// set _startPos
		_startPosition = transform.localPosition;
		
		// hide and prevent from Physics moves
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;	
		
		// initialize GUI texts 
		GUIManager.SetDistance(_distanceTraveled);
		
		if( autoMove ) {
			collider.isTrigger = true;
			rigidbody.isKinematic = true;	
		}
	}
	
	void Update () {
		
		
		if ( autoMove )
		{
			
			transform.Translate( 0.1f, 0, 0 );
			_distanceTraveled = transform.localPosition.x;
			GUIManager.SetDistance( _distanceTraveled );
			
		} else {
			
			if(  Input.GetButtonDown("Jump") )
			{
				if( _touchingPlatform )
				{
					_onDoubleJump		= false;
					_touchingPlatform 	= false; 
					rigidbody.AddForce( jumpVelocity, ForceMode.VelocityChange );
				} 
				else if ( !_onDoubleJump && rigidbody.GetRelativePointVelocity( Vector3.zero ).y > -1 )  // or is jumping ( "DoubleJump" )
				{
					_onDoubleJump		= true;
					rigidbody.AddForce( jumpVelocity * 1.2f, ForceMode.VelocityChange );
				}
			} 
			
			// update _distanceTraveled
			_distanceTraveled = transform.localPosition.x;
			
			// trigger GameOver when falling
			if( transform.localPosition.y < gameOverY ){
				GameEventManager.TriggerGameOver();
			}
			
			// update GUI texts
			GUIManager.SetDistance( _distanceTraveled );
		}
	}
	
	void FixedUpdate () {
		
		// update player physics if touching a platform
		if( _touchingPlatform ){
				
			// add acceleration to player
			rigidbody.AddForce( acceleration, 0f, 0f, ForceMode.Acceleration );
		}
	}

	void OnCollisionEnter ( Collision collision ) {
		
		if( collision.gameObject.tag == "Platform" )	// check platform collision
		{
			_touchingPlatform = true;
		}
	}

	void OnCollisionExit ( Collision collision ) {
		
		if( collision.gameObject.tag == "Platform" )	// check platform collision
			_touchingPlatform = false; 
	}
	
	void GameStart() {
		
		rigidbody.useGravity = true;
		rigidbody.WakeUp();
		renderer.enabled = true;
		
		// reset distance 
		_distanceTraveled = 0f;
		
		// reset position
		transform.localPosition = _startPosition;
		
		// show and enable movement
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
	}
	
	void GameOver() {
		
		// hide and disable movement
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;	
	}
	
	public float DistanceTraveled{ get{ return _distanceTraveled; } }
}
