using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	
	public const string JUMPER = "JUMPER";
	public const string SLIDER = "SLIDER";
	
	public Vector3 jumpingForce;
	public float speed;
	public int Ylimit;
	
	private string type;
	private int direction = -1;
	private Transform platform;
	private bool _touchingPlatform;
	private string[] types = { JUMPER, SLIDER };
	
	public void Setup( Transform platfrm )
	{
		platform = platfrm;
		type	 = types[ Random.Range( 0, types.Length) ];
		
		rigidbody.isKinematic = ( type == JUMPER ) ? false : true;
		rigidbody.WakeUp();
	}
	
	void Update () {
		
		if( type == SLIDER && platform != null )
		{
			if (transform.localPosition.x + transform.localScale.x/2 > platform.position.x + platform.localScale.x / 2 || 
				transform.localPosition.x - transform.localScale.x/2 < platform.position.x - platform.localScale.x / 2 )
				direction *= -1; 
			
			transform.Translate( speed * direction, 0, 0 );
		}
		
		if ( type == JUMPER && _touchingPlatform )
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
