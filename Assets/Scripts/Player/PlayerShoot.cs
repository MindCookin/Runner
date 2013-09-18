using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour {

	public Transform quad, testTarget, bulletContainer;
	public int quantity, duration, shootingRange;
	public float delay;
	
	private Queue<Transform> queue;
	private Transform target;
	
	void Start(){
		
		queue = new Queue<Transform>( quantity );
		
		for ( int i = 0; i < quantity; i++ ) {
			Transform q = (Transform) Instantiate( quad );	
			q.parent = bulletContainer;
			q.gameObject.SetActive(false);
			queue.Enqueue( q );
		}
	}
	
	public void StartShooting() {
		InvokeRepeating( "ShootQuad", 0, delay );
		Invoke( "StopShooting", duration );
	}
	
	void ShootQuad()
	{
		SetTarget();
		
		if( target )
		{
			Vector3 direction = target.position - transform.position;
			direction.Normalize();
			
			Transform q = queue.Dequeue();
			q.position = transform.position;
			q.GetComponent<BulletMove>().direction = direction;
			q.gameObject.SetActive( true );
			queue.Enqueue( q );
		}
	}
	
	void StopShooting() {
		
		CancelInvoke("ShootQuad");
		
		Transform[] array = queue.ToArray();
		
		for ( int i = 0; i < quantity; i++ ){
			array[i].gameObject.SetActive(false);
			array[i].position = Vector3.zero;
		}
	}
	
	void SetTarget() {
		
		Collider[] hitColliders = Physics.OverlapSphere( transform.position, shootingRange );
		 
		target 		= null;
		int index 	= 0;
		
		while( index < hitColliders.Length && target == null )
		{
			target = ( hitColliders[ index ].tag == "Enemy" ) ? hitColliders[ index ].transform : null;
			index++;
		}
	}
}
