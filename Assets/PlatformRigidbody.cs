using UnityEngine;
using System.Collections;

public class PlatformRigidbody : MonoBehaviour {
	
	public Material material;
	public PhysicMaterial physicsMaterial;
	
	bool fall;
	
	public void TryToAddRigidBody(){
		
		rigidbody.Sleep();
		
		if ( !rigidbody.isKinematic )
		{
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.velocity = Vector3.zero;
			rigidbody.sleepVelocity = 0;
			rigidbody.isKinematic = true;
		}
		
		if( Random.value < LevelStateManager.GetInstance().PlatformFallPercent )
		{
			rigidbody.WakeUp();
			
			renderer.material = material;
			collider.material = physicsMaterial;	 
			
			fall = true;
		} else {
			fall = false;
		}
	}
	
	void OnCollisionEnter( Collision collision ) {
		
		if( fall && collision.gameObject.tag == "Player" )
			rigidbody.isKinematic = false;
	}
}
