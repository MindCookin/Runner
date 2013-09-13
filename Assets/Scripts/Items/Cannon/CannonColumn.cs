using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonColumn : MonoBehaviour {
	
	public int maxCannons;
	public GameObject cannon;
	
	private Vector3[] positions; 
	private GameObject[] cannons;
	
	private int quantity;
	
	void Awake() {
		
		cannons = new GameObject[ maxCannons ];
		
		for ( int i = 0; i < maxCannons; i++ )
		{	
			cannons[i] = (GameObject)Instantiate( cannon );
			cannons[i].transform.parent = transform;
		}
		
		update();
	}
	
	public void update() {	
		
		quantity = Random.Range( 1, maxCannons + 1 );
		setPositions();
		
		place();
	}
	
	void place() {
		
		
		for ( int i = 0; i < cannons.Length; i++ )
		{	
			if ( i < quantity )
			{
				cannons[i].transform.localPosition = positions[i];
				cannons[i].GetComponent<CannonMove>().ClearNearbyCannons();
				cannons[i].SetActive(true);
				cannons[i].GetComponentInChildren<CannonPicker>().Enable();
			}
			else
			{
				cannons[i].GetComponent<CannonMove>().ClearNearbyCannons();
				cannons[i].SetActive(false);
			}
		}
	}
	
	private void setPositions()
	{
		positions = new Vector3[ quantity ];
		
		switch( quantity ) {
			case 1 : 
				positions[0] = new Vector3( 0, 0, 0 );
				break;
			case 2 : 
				positions[0] = new Vector3( 0, 3, 0 );
				positions[1] = new Vector3( 0, -3, 0 );
				break;
			case 3 : 
				positions[0] = new Vector3( 0, 4, 0 );
				positions[1] = new Vector3( 0, 0, 0 );
				positions[2] = new Vector3( 0, -4, 0 );
				break;
		}
	}
}
