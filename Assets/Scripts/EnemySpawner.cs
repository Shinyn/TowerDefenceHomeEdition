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
    [SerializeField]
    int waveCounter;
    bool spawning = false;

    public List<Rigidbody2D> enemyPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> goblinPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> orcPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> ogrePool = new List<Rigidbody2D>();
    public List<Rigidbody2D> ghostPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> destroyerPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> reaperPool = new List<Rigidbody2D>();

    int internalCount1 = 0;
    int internalCount2 = 0;
    int internalCount3 = 0;
    int internalCount4 = 0;
    int internalCount5 = 0;
    int internalCount6 = 0;
    int internalCount7 = 0;

    int enemyPoolSize = 50;

    // Ha olika pooler för olika fiender
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

    // spawning = true i start - SpawnWave i update - if spawning == true 
    // waveCounter++ i start - 
    // int wave = 0; wave++; i start.
    // if (wave1 && doneSpawning == false) - SpawnEnemy(); // when amountOfSpecificEnemyToBeSpawned is full - doneSpawning == true; wave++;
    // public RigidBody2D SpawnEnemy(RigidBody2D enemyPool, int amountOfSpecificEnemyToBeSpawned) {
    // for ( int i = 0; i < amountOfSpecificEnemyToBeSpawned; i++) return GetEnenmy(enemyPool) }

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

    IEnumerator SpawnEnemy() // Spawna fiender beroende på våg
    {
        while (waveCounter < maxLevel) // bool winner = true; när maxlevel = true && doneSpawning
        {
            // Borde döpa om till - Wave (alla fiendeTyper i en våg), enemieAmountToSpawn (mängden av en viss typ av fiende), )
            //Rigidbody2D rb = EnemyToGet();
            // if (wave1) {SpawnWave(enemyPool (goblinPool), amountToSpawn (20)) else if (wave2) {}}
            switch (waveCounter)
            {
                case 1: // Första vågen
                    // Spawna typer:
                    switch (typeSelection)
                    {
                        case "goblin":
                            amountToSpawn = 20;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                
                                Rigidbody2D rb = SpawnWave(goblinPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + amountToSpawn);
                                }
                            }
                            typeSelection = "orc";
                            yield return new WaitForSeconds(delay);
                            break;
                        case "orc":
                            amountToSpawn = 10;
                            for (int i = 0; i < amountToSpawn; i++)
                            {
                                Rigidbody2D rb = SpawnWave(orcPool, amountToSpawn);
                                if (rb != null)
                                {
                                    rb.transform.position = transform.position;
                                    yield return new WaitForSeconds(0.5f);
                                    Debug.Log("ATS " + amountToSpawn);
                                }
                            }
                            waveCounter++; // Fungerar!!! :D
                            //typeSelection = "ogre"; - Krashar unity om det inte finns "ogre" i switch
                            yield return new WaitForSeconds(delay);
                            break;
                    }
                    //waveCounter++; -- krashar unity då det förmodligen blir en oändlig loop
                    break;

                case 2:
                    // våg 2
                    Debug.Log("Wave 2");
                    yield return new WaitForSeconds(1f);
                    break;
                case 3:
                    // våg 3
                    break;
                case 4:
                    //våg 4
                    break;
                case 5:
                    // våg 5
                    break;
                    
            }
            /*Rigidbody2D rb = SpawnWave();
            if (rb != null)
            {
                rb.transform.position = transform.position;
            }
            yield return new WaitForSeconds(delay); */
        }
    }

    /*
    private Rigidbody2D EnemyToGet()
    {
        Debug.Log("enemyToGet & wavecount: " + waveCounter);
        switch (waveCounter)
        {
            // Kanske if (wave == 1) spawna 20 goblins och 10 orcs
            // else if (wave == 2) spawna 30 goblins och 5 draupnir
            // else if 
            case 1:
                Debug.Log("wave 1"); // Peta in nåt annat krav för att spawna olika monsters i samma våg
                // Spawna våg 1
                for (int i = 0; i < 20; i++)
                {
                    internalCount1++;
                    Debug.Log("internalCount: " + internalCount1);
                    if (internalCount1 >= 2)
                        waveCounter++;
                    return GetEnemy(enemyPool);
                }
                break;
            case 2:
                Debug.Log("wave 2");
                // våg 2
                for (int i = 0; i < 2; i++)
                {
                    internalCount2++;
                    if (internalCount2 >= 2)
                        waveCounter++;
                     return GetEnemy(goblinPool);
                }
                break;
            case 3:
                // våg 3
                for (int i = 0; i < 2; i++)
                {
                    internalCount3++;
                    if (internalCount3 >= 2)
                        waveCounter++;
                    return GetEnemy(orcPool);
                }
                break;
            case 4:
                // våg 4
                for (int i = 0; i < 2; i++)
                {
                    internalCount4++;
                    if (internalCount4 >= 2)
                        waveCounter++;
                    return GetEnemy(ogrePool);
                }
                break;
            case 5:
                // våg 5
                for (int i = 0; i < 2; i++)
                {
                    internalCount5++;
                    if (internalCount5 >= 2)
                        waveCounter++;
                    return GetEnemy(ghostPool);
                }
                break;
            case 6:
                // våg 6
                for (int i = 0; i < 2; i++)
                {
                    internalCount6++;
                    if (internalCount6 >= 2)
                        waveCounter++;
                    return GetEnemy(destroyerPool);
                }
                break;
            case 7:
                // våg 7
                for (int i = 0; i < 2; i++)
                {
                    internalCount7++;
                    if (internalCount7 >= 2)
                        waveCounter = 1;
                    return GetEnemy(reaperPool);
                }
                Debug.Log("waveCount is now 0");
                break;
        }
        Debug.Log("will return null now");
        return null;
    } */

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
