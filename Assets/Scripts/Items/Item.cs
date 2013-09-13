using UnityEngine;
using System.Collections;

public class Item: MonoBehaviour {

	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;
	
	private Runner runner;
	
	void Awake() {
	
		GameObject rnr = GameObject.FindGameObjectWithTag("Player");
		runner = rnr.GetComponent<Runner>();
	}
	
	void Start () {
		// add listeners
		GameEventManager.GameOver += GameOver;
		
		// deactivate object
		gameObject.SetActive(false);
	}
	
	void Update () {
		
		// if we don't see the booster, just deactivate it
		if( transform.localPosition.x + recycleOffset < runner.DistanceTraveled ){
			gameObject.SetActive(false);
			return;
		}
		
		// rotate the booster nicely
		transform.Rotate(rotationVelocity * Time.deltaTime);
	}

	public void SpawnIfAvailable (Vector3 position) {
		
		// if gameObject is already active return, 
		// also return if the spawn chance is lower than a random number
		if(gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f)) {
			return;
		}
		
		// if we pass the test, then it's time to spawn!
		transform.localPosition = position + offset;
		gameObject.SetActive(true);
	}

	private void GameOver () {
		
		// deactivate on game over
		gameObject.SetActive(false);
	}
}