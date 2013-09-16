using UnityEngine;
using System.Collections;

public class MissileMove : MonoBehaviour {
	
	public Vector3 pointToRotateAround, axisRotation, eulerAngle;
	public float angle;
	
	// Update is called once per frame
	void Update () {
	//	transform.RotateAround( pointToRotateAround, axisRotation, angle );		
		transform.Rotate( eulerAngle, Space.Self ); 
	}
}
