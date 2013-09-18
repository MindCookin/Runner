using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float inverseRotateSpeed;
	
	Transform player;
	Vector3 initialPosition;
	Quaternion initialRotation;
	
	void Awake () {
		
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		initialPosition = transform.position;
		initialRotation	= transform.rotation;
	}
	
	void Update () {
		
		transform.position = player.position + initialPosition;
		transform.rotation = initialRotation;
		transform.Rotate( player.position.y / inverseRotateSpeed, 0, 0 );
	}
}
