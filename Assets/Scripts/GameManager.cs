using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int lives;
    int gold;

    // Enemy behöver prata med GameManagern

    private void Start()
    {
        lives = 20;
        gold = 300;
    }

    private void OnEnable()
    {
        EnemyController.enemyEscaped += RemoveLife;        
    }

    private void OnDisable()
    {
        EnemyController.enemyEscaped -= RemoveLife;
    }

    public void RemoveLife(int life)
    {
        Debug.Log("Lives is: " + lives);
        lives -= life;
        if (lives <= 0)
        {
            GameOver();
        }
        Debug.Log("Now it is: " + lives);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
