using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {
	
	public Vector3 direction;
	public float speed;
	
	void Update () {
		transform.position += direction * speed;
	}
	
	void OnTriggerEnter( Collider other ){
		
		if( other.tag == "Enemy" )
		{
			gameObject.SetActive( false );
			other.gameObject.SetActive( false );
		} else if ( other.tag == "Platform" )
			gameObject.SetActive( false );
	}
}
