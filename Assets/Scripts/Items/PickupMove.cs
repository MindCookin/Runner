using UnityEngine;
using System.Collections;

public class PickupMove: MonoBehaviour {

	public Vector3 rotationVelocity;
	
	void Update () {
		
		// rotate the booster nicely
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}
}