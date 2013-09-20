using UnityEngine;
using System.Collections;

public class PlayerSize : MonoBehaviour {
	
	public float speed, growSpeed, shrinkSpeed;
	public int duration, velocityDivider;
	
	private Vector3 targetSize;
	
	void Awake () {
		
		GameEventManager.GameInit += GameInit;
		
		targetSize = Vector3.one;
	}
	
	public void Grow()
	{
		targetSize += Vector3.one * growSpeed;
		targetSize.z = 1;
	}
	
	public void Shrink()
	{
		targetSize -= Vector3.one * shrinkSpeed;
		targetSize.z = 1;
		
		if ( targetSize.x < 1 )
			targetSize = Vector3.one;
	}
	
	void Update () {
	
		if( transform.localScale != targetSize )
			transform.localScale = Vector3.Lerp( transform.localScale, targetSize, speed * Time.deltaTime );	
	}
	
	void GameInit() {
		targetSize = Vector3.one;
	}
}
