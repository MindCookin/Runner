using UnityEngine;
using System.Collections;

public class Coin: MonoBehaviour {

	public Vector3 rotationVelocity;
	private Quaternion startRotation;
	
	void Start () {
		
		startRotation = transform.rotation;
		
		Disable();
	}
	
	void Update () {
		
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	private void Enable() {
		
		collider.enabled = true;
		renderer.enabled = true;
		enabled = true;
	}
	
	public void Disable() {
		
		collider.enabled = false;
		renderer.enabled = false;
		enabled = false;
	}
	
	public void Reset() {
		
		transform.rotation = startRotation;
		
		Enable();
	}
}