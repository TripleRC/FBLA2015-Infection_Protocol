using UnityEngine;
using System.Collections;

/**
 * This script handles displaying the game win animation
 * and moving to the main menu scene when the game is won
 * */
public class GameWinManager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public float restartDelay = 5f; //5f;
	
	Animator anim;
	float restartTimer;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (EnemyManager.enemiesToSpawn == 0 && EnemyManager.enemyCount == 0 && playerHealth.currentHealth > 0)
		{
			anim.SetTrigger("GameWin");
			Debug.Log("game won");
			restartTimer += Time.deltaTime;
			if(restartTimer >= restartDelay) {
				// Application.LoadLevel(Application.loadedLevel);
				Application.LoadLevel("main menu");
			}
		}
	}
}
