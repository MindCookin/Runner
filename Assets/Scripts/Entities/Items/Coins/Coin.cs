using UnityEngine;
using System.Collections;

public class Coin: MonoBehaviour {

	public Vector3 rotationVelocity;
	private Quaternion startRotation;
	
	void Start () {
		
		startRotation = transform.rotation;
		
		enabled = false;
		renderer.enabled = false;
	}
	
	void Update () {
		
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	private void Enable() {
		
		renderer.enabled = true;
		enabled = true;
	}
	
	public void Disable() {
		
		renderer.enabled = false;
		enabled = false;
	}
	
	public void Reset() {
		
		transform.rotation = startRotation;
		
		renderer.enabled = true;
		enabled = true;
	}
}