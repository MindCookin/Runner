using UnityEngine;
using System.Collections;

public class Coin: MonoBehaviour {

	public Vector3 rotationVelocity;
	private Quaternion startRotation;
	
	void Start () {
		
		startRotation = transform.rotation;
		
		gameObject.SetActive(false);
	}
	
	void Update () {
		
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	private void Enable() {

		gameObject.SetActive(true);
	}
	
	public void Disable() {
		
		gameObject.SetActive(false);
	}
	
	public void Reset() {
		
		transform.rotation = startRotation;
		
		gameObject.SetActive( true );
	}
}