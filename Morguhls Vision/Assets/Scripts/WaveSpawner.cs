using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {NULL, SPAWNING, WAITING, COUNTING};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    int nextWave = 0;


    public Transform[] spawnPoints;

    public float timeBetweenWave = 2;
    public float waveCountdown;
    float searchCountdown = 1;

    SpawnState state = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWave;
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyStillAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING && Gamemanager.GM.villageGuardsTransform.Count <= 0)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyStillAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1;
            if (GameObject.FindGameObjectWithTag("WaveEnemy") == null)
            {
                return false;
            }
        }
        
        return true;
    }

    void WaveCompleted()
    {

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWave;

        if(nextWave + 1 > waves.Length - 1)
        {
            //StopCoroutine(SpawnWave(waves[nextWave]));
            Destroy(gameObject);
            // Wave ende
        }
        else
        {
            nextWave++;
        }

    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, sp.position, sp.rotation);
    }
}
