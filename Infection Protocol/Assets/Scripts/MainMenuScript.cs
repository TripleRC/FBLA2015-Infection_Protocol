using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public Text title;
	public Text titleSmall;
	public Text credit;
	public Text about;
	public Text controls;


	public void OnClickPlay(){
		Application.LoadLevel("level 01");
	}

	public void OnClickAbout(){
		about.color = Color.white;
		titleSmall.color = Color.white;

		title.color = Color.clear;
		credit.color = Color.clear;
		controls.color = Color.clear;
	}
	
	public void OnClickControls(){
		controls.color = Color.white;
		titleSmall.color = Color.white;
		credit.color = Color.white;
	
		title.color = Color.clear;
		about.color = Color.clear;
	}

	public void OnClickQuit(){
		Application.Quit();
	}
}
