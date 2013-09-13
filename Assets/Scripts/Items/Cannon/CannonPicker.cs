using UnityEngine;
using System.Collections;

public class CannonPicker : MonoBehaviour {
	
	public bool filled = false;
	private GameObject player;
	
	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");	
	}
	
	public void Fill() {
		
		filled = true;
		player.rigidbody.Sleep();
		player.transform.position = transform.position;
		player.transform.renderer.enabled = false;
	}
	
	public void Remove () {
		
		player.transform.renderer.enabled = true;
		filled = false;
		
		collider.enabled = false;
	}
	
	public void Enable() {
		collider.enabled = true;
	}
	
	public GameObject ball{ get{ return player; } }
}
