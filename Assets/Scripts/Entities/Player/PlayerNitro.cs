using UnityEngine;
using System.Collections;

public class PlayerNitro : MonoBehaviour
{
	public Vector3 force, torque;
	
	public void Explode()
	{ 	
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.velocity = Vector3.zero;
		rigidbody.sleepVelocity = 0;
		
		rigidbody.AddForce( force, ForceMode.Impulse );
		rigidbody.AddTorque( torque, ForceMode.Impulse );
	}
}

