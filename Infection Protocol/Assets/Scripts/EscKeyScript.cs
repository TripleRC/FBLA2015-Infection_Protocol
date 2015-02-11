using UnityEngine;
using System.Collections;

public class EscKeyScript : MonoBehaviour {

	// Update is called once per frame
	// If Esc is pressed, quit application
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Debug.Log("esc pressed");
			Application.Quit();
		}
	}
}
