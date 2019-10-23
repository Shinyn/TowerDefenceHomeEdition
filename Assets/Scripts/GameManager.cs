using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int gold;
    public TextMeshPro livesText;
    public TextMeshPro goldText;
    AudioSource soundTrack;
    AudioSource lifeLost;
    AudioSource goblinDeath;
    AudioSource gameOver;
    public EnemySpawner enemySpawner;
    public GameObject gameOverScreen;

    private void Start()
    {
        gameOverScreen.SetActive(false);
        AudioSource[] audioClips = GetComponents<AudioSource>();
        soundTrack = audioClips[0];
        lifeLost = audioClips[1];
        goblinDeath = audioClips[2];
        gameOver = audioClips[3];
        soundTrack.Play();
        lives = 20;
        gold = 1000;
        livesText.text = lives.ToString();
    }

    private void OnEnable()
    {
        EnemyController.enemyEscaped += RemoveLife;
        EnemyController.enemyKilled += AddGold;
        EnemyController.enemyDeathSound += EnemyDeathSound;
    }

    private void OnDisable()
    {
        EnemyController.enemyEscaped -= RemoveLife;
        EnemyController.enemyKilled -= AddGold;
        EnemyController.enemyDeathSound -= EnemyDeathSound;
    }

    public void RemoveLife(int life)
    {
        lives -= life;
        lifeLost.Play();
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

    public void RemoveGold(int goldSpent)
    {
        gold -= goldSpent;
        goldText.text = gold.ToString();
    }

    public void EnemyDeathSound()
    {
        goblinDeath.Play();
    }

    public void GameOver()
    {
        gameOver.Play();
        enemySpawner.gameObject.SetActive(false);
        DisableEnemies();
        soundTrack.Stop();
        gameOverScreen.SetActive(true);
        
        // 
        // foreach (enemy in enemies) {enemy.enable = false;}
    }

    private void DisableEnemies()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
