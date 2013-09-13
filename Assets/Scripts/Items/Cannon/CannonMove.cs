using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonMove : MonoBehaviour {
	
	float speed = 10f;
	public int nearbyRadius = 15;
	
	GameObject player;
	List<GameObject> nearbyCannons = new List<GameObject>();
	CannonPicker cannonPicker;
	
	float aimingCannonSeconds = 2f;
	
	GameObject aimCannon;
	float aimingCannonTimeCounter = 0;
	
	void Awake() {
		
		cannonPicker = gameObject.GetComponentInChildren<CannonPicker>();
		player		 = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	void Update () {
		
		if( !cannonPicker.filled ) {
			
			AimToTarget( player.transform );
		
			aimingCannonTimeCounter = 0;
			
		} else {
		 
			if ( nearbyCannons.Count < 1 )
				SearchNearbyCannons();
			
			SetAimCannon();
			
			AimToTarget( aimCannon.transform );
		}
	}
	
	void AimToTarget( Transform target) {
		
		Vector3 direction = target.position - transform.position;
		
		Quaternion targetRotation = Quaternion.LookRotation( direction, Vector3.forward );
		transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, speed * Time.deltaTime );
	}
	
	void SearchNearbyCannons() {
		
		Collider[] hitColliders = Physics.OverlapSphere( transform.position, nearbyRadius );
		
		int i = 0;
        while (i < hitColliders.Length) {
			
			if ( 	hitColliders[i].gameObject.tag == "Cannon" 	// check if collider is a Cannon
					&& ColliderIsNotInHere( hitColliders[i] ) 	// check if collider don't belong to this Cannon
					&& hitColliders[i].transform.position.x >= transform.position.x// check if collider position is farther away than this Cannon's position
				)
				nearbyCannons.Add( hitColliders[i].gameObject );
			
            i++;
        }
		
		if( nearbyCannons.Count > 0 )
			aimCannon = nearbyCannons[0];
	}
	
	void SetAimCannon() {
		
		if( nearbyCannons.Count > 0 )
		{	
				
			if( Mathf.FloorToInt( aimingCannonTimeCounter % aimingCannonSeconds ) == aimingCannonSeconds -1  )
			{
				aimingCannonTimeCounter = 0;
				
				int index = nearbyCannons.IndexOf( aimCannon );
				index = ( index == nearbyCannons.Count-1 ) ? 0 : index + 1;
				aimCannon = nearbyCannons[index];
			}
			
			aimingCannonTimeCounter += Time.deltaTime;
		}
	}
	
	public void ClearNearbyCannons() {
		nearbyCannons.Clear();	
	}
	
	bool ColliderIsNotInHere( Collider col ) {
		
		return !col.transform.IsChildOf( transform );
	}
}
