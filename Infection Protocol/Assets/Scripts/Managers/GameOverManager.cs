using UnityEngine;

/**
 * This script makes the game over animation appear when the player dies, then
 * sets the level to the main menu
 * */
public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
	public float restartDelay = 2f; //5f;

    Animator anim;
	float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
			restartTimer += Time.deltaTime;
			if(restartTimer >= restartDelay) {
				// Application.LoadLevel(Application.loadedLevel);
				Application.LoadLevel("main menu");
			}
        }
    }
}
