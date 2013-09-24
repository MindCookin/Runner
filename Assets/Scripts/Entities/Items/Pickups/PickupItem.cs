using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour
{
	public Material[] materials;
	public string type;
	
	void Start () {
		
		GameEventManager.GameOver += GameOver;
		
		Disable();
	}

	public void Reset() {
		
		int index = Random.Range( 0, materials.Length );
		renderer.material = materials[ index ];
			
		Enable();
	}
	
	public void Picked() {
		Disable();
	}

	void GameOver () {
		Disable();
	}
	
	public void Disable() {
		enabled = false;
		renderer.enabled = false;
		collider.enabled = false;	
	}
	
	public void Enable() {
		enabled = true;
		renderer.enabled = true;
		collider.enabled = true;	
	}
}

