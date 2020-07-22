using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject[] enemy;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 6f;
    public float waveCountDown;

    public SpawnState state = SpawnState.COUNTING;
    private float enemySearch = 1f;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpanWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    private void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        nextWave++;
        if (nextWave >= waves.Length)
        {
            nextWave = 0;
            SendMessage("RoundComplete");
        }
    }

    IEnumerator SpanWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        int rowOffset = 0;
        for (int i = 0; i < _wave.enemy.Length; i++)
        {
            rowOffset = SpawnEnemy(_wave.enemy[i], i, rowOffset);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    private int SpawnEnemy(GameObject _enemy, int index, int rowOffset)
    {
        Vector2 startingLocation = new Vector2(-8 + _enemy.transform.localScale.x / 4f, 3 + _enemy.transform.localScale.y / 4f);
        float xOffset = (((index + rowOffset) % 11) * (0.5f + _enemy.transform.localScale.x));
        float yOffset = (((index + rowOffset) / 11) * -((float)Math.Ceiling(_enemy.transform.localScale.y)));
        if (_enemy.name.Equals("Boss"))
        {
            rowOffset += 10;
        }
        Instantiate(_enemy, startingLocation + new Vector2(xOffset + (0.5f * (yOffset%2)), yOffset), Quaternion.identity);
        return rowOffset;
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

}
