using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public HealthBar waveTimer;
    public static bool canFire;
    public int round = 0;
    public int score = 0;
    private float playerSearch = 1f;
    private GameObject BossRef;
    private GameObject EnemyRef;
    public Text scoreText;
    public Text roundText;
    private float timeBeforeWave;

    private void OnEnable()
    {
        BossRef = (GameObject)Resources.Load("Prefabs/Boss");
        EnemyRef = (GameObject)Resources.Load("Prefabs/Enemy");

        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        CreateRound();
        scoreText.text = "Score: 0";

        timeBeforeWave = (int)waveSpawner.timeBetweenWaves;
        waveTimer.SetMaxHealth(timeBeforeWave);
    }

    private void Update()
    {
        waveTimer.gameObject.SetActive((waveSpawner.state == WaveSpawner.SpawnState.COUNTING));
        waveTimer.SetHealth(waveSpawner.waveCountDown);
        
        canFire = (waveSpawner.state == WaveSpawner.SpawnState.WAITING);

        if (!PlayerIsAlive())
        {
            GameOver();
        }
    }

    private void CreateRound()
    { 
        EnemyRef.GetComponentInChildren<Shoot>().force = -5f * round * .5f;
        BossRef.GetComponentInChildren<Shoot>().force = -1f * round * .25f;
        round++;
        roundText.text = "Round: " + round.ToString();
        
        WaveSpawner.Wave level = new WaveSpawner.Wave
        {
            name = "Level " + round,
            enemy = new GameObject[33]
        };
        for (int i = 0; i < level.enemy.Length; i++)
        {
            level.enemy[i] = EnemyRef;
        }

        WaveSpawner.Wave levelBoss = new WaveSpawner.Wave
        {
            name = "Level " + round + " Boss",
            enemy = new GameObject[12]
        };
        levelBoss.enemy[0] = BossRef;
        for (int i = 1; i < levelBoss.enemy.Length; i++)
        {
            levelBoss.enemy[i] = EnemyRef;
        }

        waveSpawner.waves = new WaveSpawner.Wave[] { level, levelBoss };
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
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }
}
