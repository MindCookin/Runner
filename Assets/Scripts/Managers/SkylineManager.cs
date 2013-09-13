using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkylineManager : MonoBehaviour {

	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize;
	
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	
	private PlayerMove runner;
	
	void Awake(){
		
		// find player and initialize Runner
		GameObject rnr = GameObject.FindGameObjectWithTag("Player");
		runner = rnr.GetComponent<PlayerMove>();
	}
	
	void Start () {
		
		// listen to Events
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		// disable for performance
		enabled = false;
		
		//initialize queue
		objectQueue = new Queue<Transform>(numberOfObjects);
		
		// add objects to queue
		for (int i = 0; i < numberOfObjects; i++) 
		{
			Transform obj = (Transform)Instantiate(prefab);
			obj.parent = transform;
			objectQueue.Enqueue( obj );
			objectQueue.ToArray()[i].renderer.enabled = false;
		}
		
	}
	
	void Update (){
		
		Transform obj = objectQueue.Peek();
		
		// check if objectPosition is higher than our offset (i.e. is off screen);
		if (obj.localPosition.x + recycleOffset < runner.DistanceTraveled )
			Recycle();
	}	
	
	void Recycle(){
		
		// setup building scale
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));
		
		// update position
		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		
		// update object
		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue(o);
	}
	
	void GameStart() {	
		// set starting position
		nextPosition = startPosition;
		
		// locate objects
		for (int i = 0; i < numberOfObjects; i++) 
			objectQueue.ToArray()[i].renderer.enabled = true;
		
		for (int i = 0; i < numberOfObjects; i++) 
			Recycle();
		
		// enable to play
		enabled = true;
	}
	
	
	private void GameOver () {
		enabled = false;
	}
}
