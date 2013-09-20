using UnityEngine;
using System.Collections;

public class EnemyPlace : MonoBehaviour {
	
	private Vector3 initialPosition; 
	
	public void Place( Transform platform ){
		
		rigidbody.Sleep();
		
		transform.rotation = Quaternion.identity;
		
		Vector3 targetScale = Vector3.one * Random.Range( LevelStateManager.GetInstance().EnemyMinSize, LevelStateManager.GetInstance().EnemyMaxSize );
		targetScale.z = 1;
		transform.localScale = targetScale;
		
		initialPosition = platform.localPosition;
		initialPosition.y += transform.localScale.y;
		transform.position = initialPosition;
		
		if( GetComponent<EnemySliderMove>() )
			GetComponent<EnemySliderMove>().Setup( platform );
		else 
			GetComponent<EnemyBouncerMove>().Setup( platform );
		
		gameObject.SetActive(true);
	}
}
