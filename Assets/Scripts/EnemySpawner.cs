using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // goblin
    public GameObject enemyPrefab1; // Orginal Poolen aka: enemy
    public GameObject enemyPrefab2; // orc
    public GameObject enemyPrefab3; // ogre
    public GameObject enemyPrefab4; // ghost
    public GameObject enemyPrefab5; // destroyer
    public GameObject enemyPrefab6; // reaper
    float delay = 0.5f;
    public GameManager gameManager;
    [SerializeField]
    int waveCounter;

    public List<Rigidbody2D> enemyPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> goblinPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> orcPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> ogrePool = new List<Rigidbody2D>();
    public List<Rigidbody2D> ghostPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> destroyerPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> reaperPool = new List<Rigidbody2D>();

    // Ha olika pooler för olika fiender
    int enemyPoolSize = 50;
    int goblinPoolSize = 50;
    int orcPoolSize = 50;
    int ogrePoolSize = 50;
    int ghostPoolSize = 50;
    int destroyerPoolSize = 50;
    int reaperPoolSize = 50;
    //bool expandableEnemyPool = true;

    private void Start()
    {
        waveCounter = 1;
        AddEnemiesToPool();
        StartCoroutine(SpawnEnemy());
    }

    void AddAllEnemies(int poolSize, GameObject prefab, List<Rigidbody2D> actualPool)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(prefab);
            enemy.SetActive(false);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            actualPool.Add(rb);
        }
    }

    private void AddEnemiesToPool()
    {
        AddAllEnemies(enemyPoolSize, enemyPrefab1, enemyPool);
        AddAllEnemies(goblinPoolSize, enemyPrefab, goblinPool);
        AddAllEnemies(orcPoolSize, enemyPrefab2, orcPool);
        AddAllEnemies(ogrePoolSize, enemyPrefab3, ogrePool);
        AddAllEnemies(ghostPoolSize, enemyPrefab4, ghostPool);
        AddAllEnemies(destroyerPoolSize, enemyPrefab5, destroyerPool);
        AddAllEnemies(reaperPoolSize, enemyPrefab6, reaperPool);
    }

    Rigidbody2D GetEnemy(List<Rigidbody2D> theEnemyPool)
    {
        foreach (Rigidbody2D enemy in theEnemyPool)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }
        return null;
    }

    public Rigidbody2D SpawnWave(List<Rigidbody2D> theEnemyPool, int theAmountToSpawn)
    {
        for (int i = 0; i < theAmountToSpawn; i++)
        {
            return GetEnemy(theEnemyPool);
        }
        return null;
    }
    string typeSelection = "goblin";
    int maxLevel = 5;
    int amountToSpawn = 0;
    
    float timeToWait = 1f;
    bool timeToSpawn = false;

    /*
    private IEnumerator tmp(int NRtoSpawn, List<Rigidbody2D> pool, float yieldTime)
    {
        for (int i = 0; i < NRtoSpawn; i++)
        {
            Rigidbody2D rb = SpawnWave(pool, NRtoSpawn);
            if (rb != null)
            rb.transform.position = transform.position;
            yield return new WaitForSeconds(yieldTime);
        }
    }
    */

    IEnumerator SpawnEnemy() // Spawna fiender beroende på våg
    {
        while (waveCounter <= maxLevel)
        {
            switch (waveCounter)
            {
                case 1: // Första vågen
                    // Spawna typer:
                    Debug.Log("wave 1");
                    switch (typeSelection)
                    {
                        case "goblin":
                            amountToSpawn = 2;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                
                                Rigidbody2D rb = SpawnWave(goblinPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                }
                            }
                            typeSelection = "orc";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "orc":
                            amountToSpawn = 1;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(orcPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                }
                            }
                            waveCounter++; // Fungerar!!! :D
                            Debug.Log("waveCounter");
                            //typeSelection = "ogre"; - Krashar unity om det inte finns "ogre" i switch
                            yield return new WaitForSeconds(delay);
                            break;
                    }
                    //waveCounter++; -- krashar unity då det förmodligen blir en oändlig loop
                    break;

                case 2:
                    // våg 2
                    Debug.Log("wave 2");
                    switch (typeSelection)
                    {
                        case "goblin":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {

                                Rigidbody2D rb = SpawnWave(goblinPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "orc";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "orc":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(orcPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "ogre";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "ogre":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(ogrePool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "goblin";
                            waveCounter++;
                            break;
                    }
                    yield return new WaitForSeconds(1f);
                    break;
                case 3:
                    // våg 3
                    Debug.Log("wave 3");
                    switch (typeSelection)
                    {
                        case "goblin":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {

                                Rigidbody2D rb = SpawnWave(goblinPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "orc";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "orc":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(orcPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "ogre";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "ogre":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(ogrePool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "goblin";
                            waveCounter++;
                            break;
                    }
                    yield return new WaitForSeconds(1f);
                    break;
                case 4:
                    //våg 4
                    Debug.Log("wave 4");
                    switch (typeSelection)
                    {
                        case "goblin":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {

                                Rigidbody2D rb = SpawnWave(goblinPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "orc";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "orc":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(orcPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "ogre";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "ogre":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(ogrePool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "goblin";
                            waveCounter++;
                            break;
                    }
                    yield return new WaitForSeconds(1f);
                    break;
                case 5:
                    // våg 5
                    Debug.Log("wave 5");
                    switch (typeSelection)
                    {
                        case "goblin":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {

                                Rigidbody2D rb = SpawnWave(goblinPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "orc";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "orc":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(orcPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            typeSelection = "ogre";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "ogre":
                            amountToSpawn = 5;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(ogrePool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + i);
                                }
                            }
                            waveCounter++;
                            break;
                    }
                    yield return new WaitForSeconds(1f);
                    break;
                    
            } // switch
        } // while loop -/ I en while-loop så behövde alla cases öka waveCountern annars hopper den aldrig ur, blir infinite och crashar unity
        // Efter while loop - win!
        if (waveCounter >= 5)
        {
            //gameManager.Win();
        }
    }

    private void Update()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        if (waveCounter >= 5 && enemies.Length <= 0)
        {
            gameManager.Win();
            waveCounter = -1;
        }
    }
    /*
    if (expandableEnemyPool)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        enemyPool.Add(rb);
        enemy.SetActive(true);
        return rb;
    } */
}
