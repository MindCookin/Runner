using UnityEngine;
using System.Collections;

public class DropPickupMove: MonoBehaviour {

	public Vector3 rotationVelocity, gravityVelocity;
	
	private PlayerMove player;
	
	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
	}
	
	void Update() 
	{
		transform.Translate( gravityVelocity * Time.deltaTime, Space.World );
		transform.Rotate( rotationVelocity * Time.deltaTime,Space.Self );
		
		if( transform.localPosition.y < -20 )
			gameObject.SetActive(false);
		
		if( transform.localPosition.x + 50 < player.DistanceTraveled )
			gameObject.SetActive(false);
	}
}