using UnityEngine;
using System.Collections;

/**
 * This script makes the game go to the main menu
 * when esc is pressed if you're in a level
 * 
 * It makes the game quit if esc is pressed when in the main menu
 * */
public class EscKeyScript : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			if(Application.loadedLevelName == "main menu") {
				Application.Quit();
			}
			else if(Application.loadedLevelName == "level 01" || Application.loadedLevelName == "tutorial") {
				Application.LoadLevel("main menu");
			}

			Debug.Log("esc pressed");
		}
	}
}
