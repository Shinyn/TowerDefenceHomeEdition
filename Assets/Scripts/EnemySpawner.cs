using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int speed = 150;
    public float delay = 1.0f;

    public List<Rigidbody2D> enemyPool = new List<Rigidbody2D>();
    int enemyPoolSize = 500;
    bool expandableEnemyPool = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void SpawnExtraEnemies()
    {
        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            enemyPool.Add(rb);
        }
    }

    Rigidbody2D GetEnemy()
    {
        foreach(Rigidbody2D enemy in enemyPool)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                return enemy;
            }
        }

        if (expandableEnemyPool)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            enemyPool.Add(rb);
            enemy.SetActive(true);
            return rb;
        }
        return null;
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Rigidbody2D rb = GetEnemy();
            if (rb != null)
            {
                rb.transform.position = transform.position;
                //rb.AddForce(Vector2.left * speed);
            }
            yield return new WaitForSeconds(delay);
        }
       
    }



}
