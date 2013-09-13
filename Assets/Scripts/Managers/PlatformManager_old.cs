using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager_old : MonoBehaviour {

	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	public Item booster;
	
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;
	
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	
	private Runner runner;
	
	void Awake(){
		
		// find player and initialize Runner
		GameObject rnr = GameObject.FindGameObjectWithTag("Player");
		runner = rnr.GetComponent<Runner>();
	}
	
	void Start () {
		
		// listen to game events
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		//initialize queue
		objectQueue = new Queue<Transform>(numberOfObjects);
		
		// add objects to queue
		for (int i = 0; i < numberOfObjects; i++) 
			objectQueue.Enqueue((Transform)Instantiate(prefab, new Vector3(0f, 0f, -100f), Quaternion.identity));
		
		// disable for performance
		enabled = false;
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
		
		// add booster (or not)
		booster.SpawnIfAvailable(position);
		
		// update object
		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		
		// setup material
		int materialIndex = Random.Range(0, materials.Length);
		o.renderer.material = materials[materialIndex];
		o.collider.material = physicMaterials[materialIndex];
		
		objectQueue.Enqueue(o);
		
		// generate a gap and add it to nextPosition
		/*nextPosition += new Vector3(
			Random.Range(minGap.x, maxGap.x) + scale.x,
			Random.Range(minGap.y, maxGap.y),
			Random.Range(minGap.z, maxGap.z));
		*/
		// generate a gap and add it to nextPosition
		nextPosition += new Vector3(
			Random.Range(minGap.x, maxGap.x),
			Random.Range(minGap.y, maxGap.y),
			Random.Range(minGap.z, maxGap.z));
		
		// adjust nextPosition y for minY and maxY
		if(nextPosition.y < minY)
			nextPosition.y = minY + maxGap.y;
		else if(nextPosition.y > maxY)
			nextPosition.y = maxY - maxGap.y;
	}
	
	void GameStart()
	{
		// set starting position
		nextPosition = startPosition;
		
		// locate objects
		for (int i = 0; i < numberOfObjects; i++) 
			Recycle();
		
		// enable to play
		enabled = true;
	}
	
	private void GameOver () {
		enabled = false;
	}
}
