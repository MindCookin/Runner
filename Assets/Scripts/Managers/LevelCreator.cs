using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour {
	
	public GameObject[] prefabs;
	
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	
	public float minSize, maxSize, minGap, maxGap, minY, maxY;
	
	private Vector3 nextPosition;
	private Queue<Transform> prefabQueue;
	
	private Runner player;

	void Awake() {
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Runner>();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;	
	}
	
	void Start () {
		
		prefabQueue = new Queue<Transform>( numberOfObjects );
		
		enabled 	= false;
	}
	
	void Update () {
		
		Transform obj = prefabQueue.Peek();
		
		if (obj.localPosition.x + recycleOffset < player.DistanceTraveled )
			Recycle();
	}
	
	void Recycle() {
	}
	
	void GameStart() {
		
		nextPosition = startPosition;
		
		for (int i = 0; i < numberOfObjects; i++) 
			Recycle();
		
		enabled 	= true;
	}
	
	void GameOver() {
	
	}
}
