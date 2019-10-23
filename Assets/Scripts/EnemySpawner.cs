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
    int waveCounter = 0;

    public List<Rigidbody2D> enemyPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> goblinPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> orcPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> ogrePool = new List<Rigidbody2D>();
    public List<Rigidbody2D> ghostPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> destroyerPool = new List<Rigidbody2D>();
    public List<Rigidbody2D> reaperPool = new List<Rigidbody2D>();

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
        AddEnemiesToPool();
        StartCoroutine(SpawnEnemy());
    }

    private void AddEnemiesToPool() // Göra en pool per fiendetyp?
    {
        for (int i = 0; i < enemyPoolSize; i++) // Instansiera alla fiende pooler här
        {
            GameObject enemy = Instantiate(enemyPrefab1);
            enemy.SetActive(false);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            enemyPool.Add(rb);
        }

        for (int i = 0; i < goblinPoolSize; i++) // Instansiera alla fiende pooler här
        {
            GameObject enemy = Instantiate(enemyPrefab); // sätt i inspektorn
            enemy.SetActive(false);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            goblinPool.Add(rb);
        }

        for (int i = 0; i < orcPoolSize; i++) // Instansiera alla fiende pooler här
        {
            GameObject enemy = Instantiate(enemyPrefab2); // sätt i inspektorn
            enemy.SetActive(false);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            orcPool.Add(rb);
        }

        for (int i = 0; i < ogrePoolSize; i++) // Instansiera alla fiende pooler här
        {
            GameObject enemy = Instantiate(enemyPrefab3); // sätt i inspektorn
            enemy.SetActive(false);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            ogrePool.Add(rb);
        }

        for (int i = 0; i < ghostPoolSize; i++) // Instansiera alla fiende pooler här
        {
            GameObject enemy = Instantiate(enemyPrefab4); // sätt i inspektorn
            enemy.SetActive(false);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            ghostPool.Add(rb);
        }

        for (int i = 0; i < destroyerPoolSize; i++) // Instansiera alla fiende pooler här
        {
            GameObject enemy = Instantiate(enemyPrefab5); // sätt i inspektorn
            enemy.SetActive(false);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            destroyerPool.Add(rb);
        }

        for (int i = 0; i < reaperPoolSize; i++) // Instansiera alla fiende pooler här
        {
            GameObject enemy = Instantiate(enemyPrefab6); // sätt i inspektorn
            enemy.SetActive(false);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            reaperPool.Add(rb);
        }
    }

    Rigidbody2D GetEnemy()
    {
        foreach(Rigidbody2D enemy in enemyPool) // Hämta alla fiender här
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        foreach (Rigidbody2D enemy in goblinPool) // Hämta alla fiender här
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        foreach (Rigidbody2D enemy in orcPool) // Hämta alla fiender här
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        foreach (Rigidbody2D enemy in ogrePool) // Hämta alla fiender här
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        foreach (Rigidbody2D enemy in ghostPool) // Hämta alla fiender här
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        foreach (Rigidbody2D enemy in destroyerPool) // Hämta alla fiender här
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        foreach (Rigidbody2D enemy in reaperPool) // Hämta alla fiender här
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
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
        return null;
    }

    IEnumerator SpawnEnemy() // Spawna fiender beroende på våg
    {
        while (true) // Behöver pausas mellan waves --- Behöver 1 spawner per fiendetyp?
        {
            Rigidbody2D rb = GetEnemy();
            if (rb != null)
            {
                rb.transform.position = transform.position;
            }
            yield return new WaitForSeconds(delay);
        }
       
    }

    public void WaveSpawner()
    {
        // Behöver göra en waveCounter
        // om t.ex wave 12 körs så ska den instansiera 20x goblin, 12x ghosts, 3x hulks och 1x warmonger
        waveCounter++;
        switch (waveCounter)
        {
            case 1:
                // Spawna våg 1
                EnemiesToInstansiate();
                break;
            case 2:
                // våg 2
                break;
            case 3:
                // våg 3
                break;
        }
    }
    
    public int EnemiesToInstansiate()
    {
        int enemiesCount = 20;
        return enemiesCount;
    }
}
