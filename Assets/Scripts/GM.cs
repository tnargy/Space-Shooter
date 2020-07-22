using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public static bool canFire;
    public static int round = 0;
    public static int score = 0;
    private float playerSearch = 1f;
    private GameObject BossRef;
    private GameObject EnemyRef;
    public Text scoreText;
    public Text roundText;


    private void OnEnable()
    {
        BossRef = (GameObject)Resources.Load("Boss");
        EnemyRef = (GameObject)Resources.Load("Enemy");

        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        CreateRound();
        scoreText.text = "Score: 0";
    }

    private void Update()
    {
        canFire = (waveSpawner.state == WaveSpawner.SpawnState.WAITING);

        if (!PlayerIsAlive())
        {
            GameOver();
        }
    }

    private void CreateRound()
    { 
        round++;
        roundText.text = "Round: " + round.ToString();

        WaveSpawner.Wave level1 = new WaveSpawner.Wave
        {
            name = "Level 1",
            enemy = new GameObject[33]
        };
        for (int i = 0; i < level1.enemy.Length; i++)
        {
            level1.enemy[i] = EnemyRef;
        }

        WaveSpawner.Wave level1Boss = new WaveSpawner.Wave
        {
            name = "Level 1 Boss",
            enemy = new GameObject[12]
        };
        level1Boss.enemy[0] = BossRef;
        for (int i = 1; i < level1Boss.enemy.Length; i++)
        {
            level1Boss.enemy[i] = EnemyRef;
        }

        waveSpawner.waves = new WaveSpawner.Wave[] { level1, level1Boss };
    }

    public void RoundComplete()
    {
        waveSpawner.waves = null;
        CreateRound();
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

    public static void GameOver()
    {
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
        scoreText.text = "Score: " + score.ToString();
    }
}
