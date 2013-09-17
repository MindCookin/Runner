using UnityEngine;
using System.Collections;

public class PlayerSize : MonoBehaviour {
	
	public static float BIG 	= 2f;
	public static float NORMAL 	= 1f;
	public static float SMALL 	= 0.75f;
	
	public float speed;
	public int duration;
	
	private Vector3 targetSize;
	
	// Use this for initialization
	void Start () {
		
		targetSize = Vector3.one * NORMAL;
	}
	
	public void ChangeSize( float size )
	{
		targetSize = Vector3.one * size;
		
		Invoke( "BackToNormal", duration );
	}
	
	// Update is called once per frame
	void Update () {
		
		if( transform.localScale != targetSize )
			transform.localScale = Vector3.Lerp( transform.localScale, targetSize, speed * Time.deltaTime ); 
	}
	
	void BackToNormal() {
		targetSize = Vector3.one * NORMAL;	
	}
}
