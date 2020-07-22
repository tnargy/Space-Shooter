using UnityEngine;

public class GM : MonoBehaviour
{
    public LevelLoader ll;
    public WaveSpawner waveSpawner;
    public static bool canFire;
    public int score = 0;
    private float playerSearch = 1f;
    private int round = 1;
    private GameObject BossRef;
    private GameObject EnemyRef;


    private void OnEnable()
    {
        BossRef = (GameObject)Resources.Load("Boss");
        EnemyRef = (GameObject)Resources.Load("Enemy");

        waveSpawner = gameObject.GetComponent<WaveSpawner>();
    }

    private void Start()
    {
        CreateWaves();
    }
    private void Update()
    {
        canFire = (waveSpawner.state == WaveSpawner.SpawnState.WAITING);

        if (round == 3)
        {
            //End Game Notice
            if (ll == null)
            {
                if (Input.anyKeyDown)
                {
                    GameOver();
                }
            }
            else
            {
                ll.LoadNextLevel();
            }
        }
        //Player died GAME OVER
        else if (!PlayerIsAlive())
        {
            GameOver();
        }
    }

    private void CreateWaves()
    { 
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
        round++;
        waveSpawner.waves = null;
        CreateWaves();
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
