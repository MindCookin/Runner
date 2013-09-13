using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {
	
	public Transform prefab;
	public float divider, mult;
	public int quantity;
	
	Transform[] objs;
	
	bool loaded = true;
	
	void Start () {
		
		objs = new Transform[ quantity ];
		
		Transform obj;
		Vector3 pos = Vector3.zero;
		
		for ( float i = 0; i < quantity; i++ )
		{
			pos.y 			= Mathf.Sin( i / divider ) * mult;
			pos.x 			+= 3;
			
			obj 			= (Transform) Instantiate(prefab);
			obj.position 	= pos;
			
			objs[ (int) i] 	= obj;
		}
	}
	
	void Update() {
	
		if( loaded )
		{
			loaded = false;
			GameEventManager.TriggerGameStart();	
		}
		
		Vector3 pos = Vector3.zero;
		int div = quantity/3;
		
		for ( int i = 0; i < quantity; i++ )
		{
			pos.x = ((i)/div ) * 1.5f;
			pos.y = ((i)%div ) * 1.5f;
			
			Debug.Log( pos );
			
			objs[i].transform.position = pos;
		}
	}
}
