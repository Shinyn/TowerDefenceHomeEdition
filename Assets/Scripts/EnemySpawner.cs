using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    int speed = 150;
    int delay = 1;

    public List<GameObject> enemyPool = new List<GameObject>();
    public int enemyPoolSize = 20;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void SpawnExtraEnemies()
    {
        for (int i = 0; i < enemyPoolSize; i++)
        {

        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = transform.position;
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.left * speed);

            yield return new WaitForSeconds(delay);
        }
       
    }



}
