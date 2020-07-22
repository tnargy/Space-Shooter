using UnityEngine;

public class GM : MonoBehaviour
{
    public LevelLoader ll;
    private float enemySearch = 1f;
    private float playerSearch = 1f;
    private int score = 0;

    private void Update()
    {
        if (!EnemyIsAlive())
        {
            //End Game Notice
            if (ll == null)
            {
                if (Input.anyKeyDown)
                {
                    GameOver();
                }
            }
            //Next level
            else
            {
                //ll.LoadNextLevel();
            }
        }
        //Player died GAME OVER
        else if (!PlayerIsAlive())
        {
            GameOver();
        }
    }

    private bool PlayerIsAlive()
    {
        playerSearch -= Time.deltaTime;
        if (playerSearch <= 0f)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                return false;
            }
        }
        return true;
    }
    private bool EnemyIsAlive()
    {
        enemySearch -= Time.deltaTime;
        if (enemySearch <= 0f)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("Loot") == null)
            {
                return false;
            }
        }
        return true;
    }

    public static void GameOver()
    {
        Debug.Log("Game Over!");

        string[] tags = { "Enemy", "Loot" };
        foreach (string tag in tags)
        {
            GameObject[] cleanHouse = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject item in cleanHouse)
            {
                Destroy(item);
            }
        }
        LevelLoader.LoadMainMenu();
    }

    public void Score(int points)
    {
        Debug.Log("Score!");
        score += points;
    }
}
