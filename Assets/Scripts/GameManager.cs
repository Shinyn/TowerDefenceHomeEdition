using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    int lives;
    int gold;
    public TextMeshPro livesText;
    public TextMeshPro goldText;

    private void Start()
    {
        lives = 20;
        gold = 300;
        livesText.text = lives.ToString();
    }

    private void OnEnable()
    {
        EnemyController.enemyEscaped += RemoveLife;
        EnemyController.enemyKilled += AddGold;
    }

    private void OnDisable()
    {
        EnemyController.enemyEscaped -= RemoveLife;
        EnemyController.enemyKilled -= AddGold;
    }

    public void RemoveLife(int life)
    {
        lives -= life;
        livesText.text = lives.ToString();
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void AddGold(int goldGained)
    {
        gold += goldGained;
        goldText.text = gold.ToString();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        // visa gameOver screen
        // disable spawner
    }
}
