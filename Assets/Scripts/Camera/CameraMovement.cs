using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float inverseRotateSpeed;
	
	Transform player;
	Vector3 initialPosition;
	Quaternion initialRotation;
	
	void Awake () {
		
		GameEventManager.GameOver 	+= GameOver;
		GameEventManager.GameStart 	+= GameStart;
		
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		initialPosition = transform.position;
		initialRotation	= transform.rotation;
	}
	
	void Update () {
		
		transform.position = player.position + initialPosition;
		transform.rotation = initialRotation;
		transform.Rotate( player.position.y / inverseRotateSpeed, 0, 0 );
	}
	
	void GameOver() {
	/*	Vector3 targetPos = player.position;
		targetPos.y = 2;
		transform.position = targetPos + initialPosition;
	*/	
	}
	
	void GameStart() {
		
	}
}
