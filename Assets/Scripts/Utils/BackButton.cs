using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}
}
