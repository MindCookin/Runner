using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour {

	public Transform quad, testTarget, bulletContainer;
	public int quantity, duration, shootingRange;
	public float delay;
	
	private Queue<Transform> queue;
	private Transform target;
	private PlayerColors colors;
	
	void Start(){
		
		colors= GetComponent<PlayerColors>();
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
		
		colors.ChangeColor( PlayerColors.RED );
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
	
	public void StopShooting() {
		
		CancelInvoke("ShootQuad");
		
		Transform[] array = queue.ToArray();
		
		for ( int i = 0; i < quantity; i++ ){
			array[i].gameObject.SetActive(false);
			array[i].position = Vector3.zero;
		}
		
		colors.BackToInitialColor();
	}
	
	void SetTarget() {
		
		Collider[] hitColliders = Physics.OverlapSphere( transform.position, shootingRange );
		
		string indexes = "";
		for ( int i = 0; i < hitColliders.Length; i++ )
		{
			if ( hitColliders[i].tag == "Enemy" )
				indexes += "," + i.ToString();
		}
		
		string[] enemies = indexes.Split(',');
		
		if ( enemies.Length > 1 )
		{
			int selected = int.Parse( enemies[ Random.Range( 1, enemies.Length ) ] );
			target = hitColliders[ selected ].transform;
		}
	}
}
