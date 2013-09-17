using UnityEngine;
using System.Collections;

public class PlayerColors : MonoBehaviour {
	
	public static Color RED = new Color(1, 0, 0 );
	public static Color GREEN = new Color(0, 1, 0 );
	public static Color BLUE = new Color(0, 0, 1 );
	public static Color YELLOW = new Color(1, 1, 0 );
	
	public int duration = 5;
	
	Color initialColor;
	
	void Start() {
		initialColor = renderer.material.color;
	}
	
	public void ChangeColor( Color color ) {
		
		renderer.material.color = color;
		
		CancelInvoke("BackToInitialColor");
		Invoke("BackToInitialColor", duration );
	}
	
	void BackToInitialColor(){
		renderer.material.color = initialColor;	
	}
}
