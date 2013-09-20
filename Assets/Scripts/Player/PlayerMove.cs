using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	
	public float 	acceleration, autoMoveAcceleration;
	public Vector3 	jumpVelocity;
	public float 	gameOverY, liftSpeed;
	public bool		autoMove;
	
	private Vector3 _startPosition;
	private float 	_distanceTraveled;
	private bool 	_touchingPlatform, _onDoubleJump, _onLift;
			
	private Vector3 constantMove, targetLift;
	private PlayerSize playerSize;
	private PlayerShoot playerShoot;
	private ForceField forceField;
	
	void Start() {
	
		
		playerShoot			= GetComponent<PlayerShoot>();
		forceField = gameObject.GetComponentInChildren<ForceField>();
		playerSize	 = GetComponent<PlayerSize>();
		
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
			rigidbody.isKinematic = true;
			transform.Translate( autoMoveAcceleration, 0, 0 );
			_distanceTraveled = transform.localPosition.x;
			GUIManager.SetDistance( _distanceTraveled );
			
		} else if ( _onLift ) {
			
			transform.position = Vector3.Lerp( transform.position, targetLift, liftSpeed * Time.deltaTime );
			transform.rotation = Quaternion.Lerp( transform.rotation, Quaternion.identity, liftSpeed * Time.deltaTime );
			
			if ( _startPosition.y - transform.position.y < 0.1f )
			{
				_startPosition.x = transform.position.x;
				transform.position = _startPosition;
				transform.rotation = Quaternion.identity;
				
				GameEventManager.TriggerGameReset();
			}
			
		} else {
			
			if(  Input.GetButtonDown("Jump") )
			{
				if( _touchingPlatform )
				{
					_onDoubleJump		= false;
				//	_touchingPlatform 	= false; 
					rigidbody.AddForce( jumpVelocity, ForceMode.VelocityChange );
				} 
				else if ( !_onDoubleJump && rigidbody.GetRelativePointVelocity( Vector3.zero ).y > -1 )  // or is jumping ( "DoubleJump" )
				{
					_onDoubleJump		= true;
					rigidbody.AddForce( jumpVelocity * 1.2f, ForceMode.VelocityChange );
				}
			} 
			
			// update _distanceTraveled
			_distanceTraveled = transform.localPosition.x - _startPosition.x;
			
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
		
		if( col.tag == "Enemy" )
		{
			if( forceField.isOn )
			{
				forceField.Hide();
			} else {
				playerShoot.StopShooting();
				playerSize.Shrink();
			}
		}
	}
	
	void OnCollisionStay ( Collision collision ) {
		
		GameObject col = collision.gameObject;
		
		if( col.tag == "Platform" )	// check platform collision
			_touchingPlatform = true;
	}

	void OnCollisionExit ( Collision collision ) {
		
		if( collision.gameObject.tag == "Platform" )	// check platform collision
			_touchingPlatform = false; 
	}
	
	void GameInit() {
		
		_onLift = false;
		
		enabled = false;	
		
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
		
		rigidbody.AddForce( jumpVelocity/2, ForceMode.VelocityChange );
		
		enabled = true;
	}
	
	void GameOver() {
		
		forceField.Hide();
		playerShoot.StopShooting();
		
		// hide and disable movement
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
		
		_distanceTraveled = 0;
	
		Invoke( "Reinit", 0.5f );
		
	//	Invoke( "Lift", 0.5f );
	}
	
	void Reinit(){
		GameEventManager.TriggerGameInit();	
	}
	
	void Lift() {
		
		targetLift = _startPosition;
		targetLift.x = transform.position.x;
		_startPosition = targetLift;
		
		LevelStateManager.GetInstance().SetInitialPosition( _startPosition );
		
		renderer.enabled = true;
		enabled = true;
		_onLift = true;
	}
	
	public float DistanceTraveled{ get{ return _distanceTraveled; } }
}
