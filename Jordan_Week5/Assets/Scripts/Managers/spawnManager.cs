using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    gameManager gm;
    
    public float spawnRange = 20f;

    [Header("Prefabs")]
    public GameObject[] enemyPrefab;
    public GameObject bossPrefab;
    public GameObject[] powerUpPrefab;

    [Header("PowerUp Spawn Timers")]
    public float spawnStartTimer;
    public float spawnRepeatTimer;

    [Header("Enemies")]
    public int totalEnemies;
    public int wave = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<gameManager>();
        SpawnEnemy(totalEnemies);
       
        InvokeRepeating("SpawnPowerUp", spawnStartTimer, spawnRepeatTimer);
    }

    // Update is called once per frame
    void Update()
    {
        totalEnemies = FindObjectsOfType<navMeshScript>().Length;
        if (totalEnemies == 0)
        {
            wave++;

            if (wave % 5 == 0 && wave != 0)
            {
                int extraBossCount = wave / 5;
                for (int i = 0; i < extraBossCount; i++)
                {
                    SpawnBoss();
                }
            }
            else
            {
                SpawnEnemy(wave);
            }

            
        }



           }

    public Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    void SpawnEnemy(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(), enemyPrefab[enemyIndex].transform.rotation);
        }
    }

    void SpawnPowerUp()
    {
        Vector3 offset = new Vector3(0, 2, 0);
        int powerupIndex = Random.Range(0, powerUpPrefab.Length);
        Instantiate(powerUpPrefab[powerupIndex], GenerateSpawnPosition() + offset, powerUpPrefab[powerupIndex].transform.rotation);
    }
    
    void SpawnBoss()
    {
        Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
    }
}
