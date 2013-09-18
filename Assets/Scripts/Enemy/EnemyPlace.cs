using UnityEngine;
using System.Collections;

public class EnemyPlace : MonoBehaviour {
	
	private Vector3 initialPosition; 
	
	LevelStateManager level;
	
	void Start() {
		level = LevelStateManager.instance;
	}
	
	public void Place( Transform platform ){
		
		rigidbody.Sleep();
		
		initialPosition = platform.localPosition;
		initialPosition.y += platform.localScale.y;
		initialPosition.x += Random.value * platform.localScale.x - platform.localScale.x/2;
		
		transform.rotation = Quaternion.identity;
		transform.position = initialPosition;
		transform.localScale = Vector3.one * Random.Range( level.EnemyMinSize, level.EnemyMinSize );
		
		if( GetComponent<EnemySliderMove>() )
			GetComponent<EnemySliderMove>().Setup( platform );
		else 
			GetComponent<EnemyBouncerMove>().Setup( platform );
		
		gameObject.SetActive(true);
	}
}
