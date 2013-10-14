using UnityEngine;
using System.Collections;

public class PickupSound : MonoBehaviour {
	
	AudioSource source;
	
	void Awake() {
		source = GetComponent<AudioSource>();	
	}
	
	public void Play() {
		source.Play();
	}
}
