using UnityEngine;
using System.Collections;

public class PlayerSize : MonoBehaviour {
	
	public static float BIG 	= 2f;
	public static float NORMAL 	= 1f;
	public static float SMALL 	= 0.75f;
	
	public float speed, minSize;
	public int duration, velocityDivider;
	
	private Vector3 targetSize;
	
	void Awake () {
		
		targetSize = Vector3.one * NORMAL;
		
		transform.localScale = Vector3.one * minSize;
	}
	
	public void ChangeSize( float size )
	{
	//	targetSize = Vector3.one * size;
		
	//	Invoke( "BackToNormal", duration );
	}
	
	void Update () {
	
		if( rigidbody.velocity.x / velocityDivider > 1f )
			targetSize = ( rigidbody.velocity.x * Vector3.one ) / velocityDivider;
		else 
			targetSize = Vector3.one;
		
		targetSize.z = 1;
		
		/*
		if( targetSize.x > minSize )
			transform.localScale = Vector3.one * targetSize.x;
		else
			transform.localScale = Vector3.one * minSize;
		*/	
			
		if( transform.localScale != targetSize )
			transform.localScale = Vector3.Lerp( transform.localScale, targetSize, speed * Time.deltaTime ); 
			
	}
	
	void BackToNormal() {
		targetSize = Vector3.one * NORMAL;	
	}
}
