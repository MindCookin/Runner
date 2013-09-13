using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
	
	public float acceleration;
	public Vector3 boostVelocity, jumpVelocity;
	public float gameOverY;
	
	private int 	_boosts;
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
		GUIManager.SetBoosts(_boosts);
	}
	
	void Update () {
		
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
	
	private void OnTriggerEnter ( Collider col ) {
		
		switch( col.gameObject.tag ) 
		{
			case "Coin":
				addCoin();
				col.gameObject.SetActive(false);
			break;
			case "Cannon":
				col.GetComponent<CannonPicker>().Fill();
			break;
		} 
	}
	
	void GameStart() {
		
		rigidbody.useGravity = true;
		rigidbody.WakeUp();
		renderer.enabled = true;
		
		// reset boosts
		_boosts = 0;
		
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
	
	void addBoost() {
		_boosts += 1;
		
		GUIManager.SetBoosts(_boosts);
	}
	
	void addCoin() {
		
		GUIManager.AddCoin();
	}
	
	public float DistanceTraveled{ get{ return _distanceTraveled; } }
}
