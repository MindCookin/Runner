using UnityEngine;
using System.Collections;

public class DropPickupMove: MonoBehaviour {

	public Vector3 rotationVelocity, gravityVelocity;
	
	private Transform cube;
	
	void Start() 
	{
		cube = transform.FindChild("Cube");
	}
	
	void Update() 
	{
		transform.Translate( gravityVelocity * Time.deltaTime );
		cube.Rotate( rotationVelocity * Time.deltaTime );
		
		if( transform.localPosition.y < -20 )
			gameObject.SetActive(false);
	}
}