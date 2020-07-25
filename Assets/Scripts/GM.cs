using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public GameObject shopUI;
    public WaveSpawner waveSpawner;
    public HealthBar waveTimer;
    public static bool canFire;
    public int round = 0;
    public int score = 0;
    public int loot = 0;
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
        round++;
        CreateRound();
        scoreText.text = "Score: 0";

        timeBeforeWave = (int)waveSpawner.timeBetweenWaves;
        waveTimer.SetMaxHealth(timeBeforeWave);
    }

    private void Update()
    {
        
        if (shopUI.activeSelf)
        {
            return;
        }    

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
        OpenShop();

        waveSpawner.waves = null;
        round++;
        AdjustDifficulty();
        CreateRound();
    }

    private void OpenShop()
    {
        shopUI.SetActive(true);
    }    

    private void AdjustDifficulty()
    {
        //Move enemies starting with round 2
        if (round == 2)
        {
            EnemyRef.GetComponent<Enemy>().moveSpeed = 1;
        }
        //Start firing back at player starting round 3
        else if (round == 3)
        { 
            EnemyRef.GetComponentInChildren<Shoot>().force = -5f * .5f;
            BossRef.GetComponentInChildren<Shoot>().force = -1f * .25f;
        }
        //Randomly increase firing or movement
        else
        {
            //Enemy Changes
            switch(Random.Range(0, 3))
            {
                case 0:
                    //Increase Speed
                    EnemyRef.GetComponent<Enemy>().moveSpeed += 0.5f;
                    break;
                case 1:
                    //Increase Bullets
                    EnemyRef.GetComponentInChildren<Shoot>().force -= 0.25f;
                    break;
                case 2:
                    //Increase Firing Rate but cap it.
                    if (EnemyRef.GetComponentInChildren<Shoot>().randomOffset > 5)
                        EnemyRef.GetComponentInChildren<Shoot>().randomOffset -= 1f;
                    break;
                default:
                    break;
            }

            //Boss Changes
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
        GameObject.Find("GM").SendMessage("checkForHighScore");
        LevelLoader.LoadMainMenu();
    }

    public void Score(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    public void Loot(int points)
    {
        loot += points;
    }

    public void checkForHighScore()
    {
        if (score > PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
}
