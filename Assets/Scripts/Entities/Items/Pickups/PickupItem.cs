using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour
{
	public Material[] materials;
	public string type;
	
	void Start () {
		
		GameEventManager.GameOver += GameOver;
		
		enabled = false;
	}

	public void Reset() {
		
		int index = Random.Range( 0, materials.Length );
		renderer.material = materials[ index ];
			
		enabled = true;
	}

	void GameOver () {
	
		enabled = false;
	}
}

