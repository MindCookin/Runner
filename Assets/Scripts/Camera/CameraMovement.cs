using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float speed;
	
	GameObject player;
	Vector3 targetPosition;
	Vector3 offset;
	
	void Awake () {
		
		player = GameObject.FindGameObjectWithTag("Player");
		offset = transform.position;
	}
	
	void Update () {
		
		targetPosition = player.transform.position + offset;
		
		transform.position = targetPosition;
	//	transform.position = Vector3.Slerp( transform.position, targetPosition, speed * Time.deltaTime );
	}
}
