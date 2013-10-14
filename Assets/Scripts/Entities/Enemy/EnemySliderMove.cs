using UnityEngine;
using System.Collections;

public class EnemySliderMove : MonoBehaviour {
	
	public float speed;
	
	int direction = -1;
	Transform platform;
	Vector3 targetPos;
	Vector3 initialScale;
	
	public void Setup( Transform pltform )
	{
		platform = pltform;
		
		initialScale = transform.localScale;
		
		rigidbody.WakeUp();
	}
	
	void Update () {
		
		if ( platform != null )
		{
			if ( direction == 1 && transform.localPosition.x + transform.localScale.x/2 > platform.position.x + platform.localScale.x / 2 )
				direction = -1;
			if ( direction == -1 && transform.localPosition.x - transform.localScale.x/2 < platform.position.x - platform.localScale.x / 2 )
				direction = 1; 
			
			transform.Translate( speed * direction, 0, 0 );
			
			targetPos = transform.position;
			targetPos.y = platform.position.y + initialScale.y;
			transform.position = targetPos;
		}
	}
}
