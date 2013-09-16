using UnityEngine;
using System.Collections;

public class SimpleItem : MonoBehaviour
{
	private PlayerMove runner;
	
	void Awake() {
	
		GameObject rnr = GameObject.FindGameObjectWithTag("Player");
		runner = rnr.GetComponent<PlayerMove>();
	}
	
	void Start () {
		
		GameEventManager.GameOver += GameOver;
		
		gameObject.SetActive(false);
	}
	
	void Update () {
		
		if( transform.localPosition.x + 50 < runner.DistanceTraveled )
			gameObject.SetActive(false);
	}

	public void Reset() {
		
		if ( gameObject.GetComponentInChildren<CannonPicker>() != null )
			 gameObject.GetComponentInChildren<CannonPicker>().Enable();
		
		gameObject.SetActive(true);
	}

	void GameOver () {
	
		gameObject.SetActive(true);
	}
}

