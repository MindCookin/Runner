using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerColors : MonoBehaviour {
	
	public static Color RED = new Color(1, 0, 0 );
	public static Color GREEN = new Color(0, 1, 0 );
	public static Color BLUE = new Color(0, 0, 1 );
	public static Color YELLOW = new Color(1, 1, 0 );
	
	public PhysicMaterial defaultMaterial;
	public PhysicMaterial[] physMaterials;
	public int duration = 5;
	
	Dictionary< Color, PhysicMaterial > colorDict;
	
	Color initialColor;
	
	void Start() {
		
		colorDict = new Dictionary<Color, PhysicMaterial>();
		colorDict[ RED ] 	= physMaterials[0];
		colorDict[ GREEN ] 	= physMaterials[1];
		colorDict[ BLUE ] 	= physMaterials[2];
		colorDict[ YELLOW ] = physMaterials[3];
		
		initialColor = renderer.material.color;
	}
	
	public void ChangeColor( Color color ) {
		
		renderer.material.color = color;
		collider.material		= colorDict[ color ];
		
		CancelInvoke("BackToInitialColor");
		Invoke("BackToInitialColor", duration );
	}
	
	void BackToInitialColor(){
		collider.material		= defaultMaterial;
		renderer.material.color = initialColor;	
	}
}
