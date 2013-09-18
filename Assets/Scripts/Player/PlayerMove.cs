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
	private PlayerColors playerColors;
	
	void Start() {
	
		playerColors = GetComponent<PlayerColors>();
		
		// listen to game events
		GameEventManager.GameInit += GameInit;
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
		
		GameObject col = collision.gameObject;
		
		if( col.tag == "Platform" || col.tag == "Enemy" )	// check platform collision
		{
		//	if( col.tag == "Platform" )
		//		SetPlayerColor( col.renderer.material.name );
			
			_touchingPlatform = true;
		}
	}

	void OnCollisionExit ( Collision collision ) {
		
		if( collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Enemy" )	// check platform collision
			_touchingPlatform = false; 
	}
	
	void GameInit() {
		
		rigidbody.isKinematic 	= true;
		rigidbody.useGravity = true;
		rigidbody.Sleep();
		
		_distanceTraveled = 0f;
		
		transform.localPosition = _startPosition;
		transform.rotation		= Quaternion.identity;
		
		renderer.enabled 		= true;
	}
	
	void GameStart() {
		
		rigidbody.WakeUp();
		rigidbody.isKinematic = false;
		
		enabled = true;
	}
	
	void GameOver() {
		
		// hide and disable movement
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;	
	}
	/*
	void SetPlayerColor( string materialName ){
		
		materialName = materialName.Split('_')[1];
		Color targetColor;
		
		switch( materialName )
		{
			case "Red" 		: targetColor = PlayerColors.RED; 	break; 
			case "Blue" 	: targetColor = PlayerColors.BLUE; 	break;
			case "Green" 	: targetColor = PlayerColors.GREEN; break;
			case "Yellow" 	: targetColor = PlayerColors.YELLOW;break;
			default			: targetColor = PlayerColors.BLUE;  break;
		}
		
		playerColors.ChangeColor( targetColor );
	}*/
	
	public float DistanceTraveled{ get{ return _distanceTraveled; } }
}
