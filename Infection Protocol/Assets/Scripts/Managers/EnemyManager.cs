using UnityEngine;

/**
 * This class periodically spawns enemies at one of the spawn
 * points dragged onto it.
 * 
 * It also handles the count of enemies left to spawn and enemies currently spawned.
 * */
public class EnemyManager : MonoBehaviour
{
	public static int enemyCount = 0;
	public static int enemiesToSpawn = 32;

	public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

		if (enemiesToSpawn <= 0)
		{
			return;
		}

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		enemyCount++;
		enemiesToSpawn--;
    }
}
