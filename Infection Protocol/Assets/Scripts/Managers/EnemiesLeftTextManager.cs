using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * This script is attached to the enemies left text
 * Updating it every frame
 * */
public class EnemiesLeftTextManager : MonoBehaviour {
	
	Text text;
	
	
	void Awake ()
	{
		text = GetComponent <Text> ();
	}
	
	
	void Update ()
	{
		text.text = "Enemies Left: " + (EnemyManager.enemiesToSpawn + EnemyManager.enemyCount);
	}
}
