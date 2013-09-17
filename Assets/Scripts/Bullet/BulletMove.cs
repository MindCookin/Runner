using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {
	
	public Vector3 direction;
	public float speed;
	
	void Update () {
		transform.position += direction * speed;
	}
}
