using UnityEngine;
using System.Collections;

public class PlayerForceField : MonoBehaviour {
	 
	ForceField forceField;

	void Awake() {
		forceField = gameObject.GetComponentInChildren<ForceField>();
	}
	
	public void Show() {
		forceField.Show();
	}
}
