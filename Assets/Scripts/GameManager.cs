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
    [HideInInspector]
    public AudioSource soundTrack;
    AudioSource lifeLost;
    AudioSource goblinDeath;
    AudioSource gameOver;
    AudioSource orcDeath;
    AudioSource ogreDeath;
    AudioSource ghostDeath;
    AudioSource destroyerDeath;
    AudioSource reaperDeath;
    AudioSource draupnirDeath;
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
        orcDeath = audioClips[4];
        ogreDeath = audioClips[5];
        ghostDeath = audioClips[6];
        destroyerDeath = audioClips[7];
        reaperDeath = audioClips[8];
        draupnirDeath = audioClips[9];
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

    public void EnemyDeathSound(string enemyName)
    {
        switch (enemyName)
        {
            case "Draupnir":
                draupnirDeath.Play();
                break;
            case "Goblin":
                goblinDeath.Play();
                break;
            case "Orc":
                orcDeath.Play();
                break;
            case "Ogre":
                ogreDeath.Play();
                break;
            case "Ghost":
                ghostDeath.Play();
                break;
            case "Destroyer":
                destroyerDeath.Play();
                break;
            case "Reaper":
                reaperDeath.Play();
                break;
        }
         // Skapa deathSounds i kod och lägg dom i en lista så dom inte avbryter varandra om fler dör samtidigt
    }

    public void GameOver()
    {
        gameOver.Play();
        enemySpawner.gameObject.SetActive(false);
        DisableEnemies();
        DisableTowers();
        soundTrack.Stop();
        gameOverScreen.SetActive(true);
    }

    private void DisableEnemies()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    private void DisableTowers()
    {
        TowerBaseController[] towers = FindObjectsOfType<TowerBaseController>();
        foreach (TowerBaseController tower in towers)
        {
            tower.gameObject.SetActive(false);
        }
    }
}
