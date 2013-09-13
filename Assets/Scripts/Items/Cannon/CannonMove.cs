using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonMove : MonoBehaviour {
	
	public Vector3 rotationVector;
	public float aimingSpeed;
	
	PlayerMove player;
	CannonPicker cannonPicker;
	private Quaternion targetRotation;
	
	void OnEnable(){
		
		player 		= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		cannonPicker= transform.GetComponentInChildren<CannonPicker>();
		
		targetRotation = Quaternion.Euler( rotationVector );
	}
	
	void Update () {
		
		if( !cannonPicker.filled )
			AimToTarget( player.transform );
		else
			transform.localRotation = Quaternion.Lerp( transform.localRotation, targetRotation, aimingSpeed * Time.deltaTime );	
	}
	
	void AimToTarget( Transform target) {
		
		Vector3 direction = target.position - transform.position;
		
		Quaternion targetRotation = Quaternion.LookRotation( direction, Vector3.forward );
		transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, aimingSpeed * Time.deltaTime );
	}
}
