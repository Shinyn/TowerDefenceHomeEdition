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
    public EnemyController enemyController; // behövs för att säga vilket guldvärde fienden har 
    public float delay = 1.0f;
    [SerializeField]
    int waveCounter;

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
    
    private Rigidbody2D EnemyToGet()
    {
        Debug.Log("enemyToGet & wavecount: " + waveCounter);
        switch (waveCounter)
        {
            case 1:
                Debug.Log("wave 1"); // Peta in nåt annat krav för att spawna olika monsters i samma våg
                // Spawna våg 1
                for (int i = 0; i < 2; i++)
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

    IEnumerator SpawnEnemy() // Spawna fiender beroende på våg
    {
        while (true) // Behöver pausas mellan waves --- Behöver 1 spawner per fiendetyp?
        {
            Rigidbody2D rb = EnemyToGet();
            if (rb != null)
            {
                rb.transform.position = transform.position;
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
