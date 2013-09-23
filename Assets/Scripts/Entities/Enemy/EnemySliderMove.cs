using UnityEngine;
using System.Collections;

public class EnemySliderMove : MonoBehaviour {
	
	public float speed;
	
	private int direction = -1;
	private Transform platform;
	
	public void Setup( Transform platfrm )
	{
		platform = platfrm;
		rigidbody.WakeUp();
	}
	
	void Update () {
		
		if( platform != null )
		{
			if ( direction == 1 && transform.localPosition.x + transform.localScale.x/2 > platform.position.x + platform.localScale.x / 2 )
				direction = -1;
			if ( direction == -1 && transform.localPosition.x - transform.localScale.x/2 < platform.position.x - platform.localScale.x / 2 )
				direction = 1; 
			
			transform.Translate( speed * direction, 0, 0 );
		}
	}
}
