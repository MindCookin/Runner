using UnityEngine;
using System.Collections;

public class PlatformPickupPlace : MonoBehaviour
{
	public int upAndDownOffset = 1;
	public Vector3 rotationVelocity;
	
	Vector3 targetPos;
	int counter = 0; 
	
	public int offsetY = 5;
	
	public Vector3 initialPosition;

	public void Place( Transform platform ){
		
		initialPosition = platform.localPosition;
		initialPosition.y += platform.localScale.y + offsetY;
		
		transform.rotation = Quaternion.identity;
		transform.Rotate( Random.value * 5, 0, Random.value * 100 );
		transform.position = initialPosition;		
		
		gameObject.SetActive(true);
	}
	
	void Update () {
		
		counter++;
		
		// rotate the booster nicely
		transform.Rotate(rotationVelocity * Time.deltaTime);
		
		targetPos = transform.position;
		targetPos.y = Mathf.Sin( Mathf.PI * counter / 180 ) * upAndDownOffset + initialPosition.y;
		transform.position = targetPos;
		
		if ( counter > 360 )
			counter = 0;
	}
	
	public void Reset() {
		counter = Random.Range( 0, 360 );
	}
}

