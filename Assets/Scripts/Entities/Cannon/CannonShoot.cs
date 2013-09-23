using UnityEngine;
using System.Collections;

public class CannonShoot : MonoBehaviour {
	
	public float power = 5f;
		
	CannonPicker cannonPicker;
	
	void Awake() {
		
		cannonPicker = gameObject.GetComponent<CannonPicker>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if( cannonPicker.filled && Input.GetButton("Jump") )
		{
			doShoot();	
		}
	}
	
	void doShoot() {
		
		cannonPicker.ball.rigidbody.WakeUp();
		cannonPicker.ball.rigidbody.AddForce( transform.up * power, ForceMode.VelocityChange );
		
		cannonPicker.Remove();
	}
}
