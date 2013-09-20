using UnityEngine;
using System.Collections;

public class ForceField : MonoBehaviour {
	
	public float speed, size;
	public int duration;
	
	private float targetIntensity;
	private Vector3 targetSize;
	private Light forceFieldLight;
	
	public bool isOn { get { return renderer.enabled; } }
	
	// Use this for initialization
	void Start () {
	
		forceFieldLight = gameObject.GetComponentInChildren<Light>();
		forceFieldLight.intensity = 0;
		
		targetIntensity 		= 0;
		targetSize				= Vector3.zero;
		
		renderer.enabled 		= false;
		transform.localScale 	= Vector3.zero;
	}
	
	public void Show()
	{
		renderer.enabled = true;
		
		targetSize 		= Vector3.one * size;
		targetIntensity	= 0.68f;
		
//		Invoke( "Hide", duration );
	}
	
	// Update is called once per frame
	void Update () {
		
		if( transform.localScale != targetSize )
		{
			transform.localScale 		= Vector3.Lerp( transform.localScale, targetSize, speed * Time.deltaTime ); 
			forceFieldLight.intensity	= Mathf.Lerp( forceFieldLight.intensity, targetIntensity, speed * Time.deltaTime );
		}
		else if ( targetSize == Vector3.zero )
			renderer.enabled = false;
	}
	
	public void Hide() {
		targetSize = Vector3.zero;
		targetIntensity = 0;
	}
}
