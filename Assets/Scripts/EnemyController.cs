using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]
    public string name;
    public int hp;
    public int lifeCost;
    public int goldValue;

    [Header("Other")]
    public GameObject goalPost;
    public bool madeItToExit = false;
    SpriteRenderer sprite; // Ha för att byta sprite beroende på riktning?
    float timeCheckDelay = 0.1f;
    bool dead = false;
    public int startHp;
    private bool neverUsed = true;
    public Image healthbar;
    public Image hpBackground;

    public delegate void EnemyEscaped(int lifeLost);
    public static event EnemyEscaped enemyEscaped;

    public delegate void EnemyKilled(int goldgained);
    public static event EnemyKilled enemyKilled;

    public delegate void EnemyDeathSound(string enemyName);
    public static event EnemyDeathSound enemyDeathSound;

    private void Start()
    {
        healthbar.enabled = false;
        hpBackground.enabled = false;
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
      if (neverUsed)
            startHp = hp;
      else
            hp = startHp;
    }

    private void OnDisable()
    {
        neverUsed = false;
    }

    private void Update()
    {
        if (madeItToExit == true)
        {
            enemyEscaped?.Invoke(lifeCost);
            healthbar.enabled = false;
            hpBackground.enabled = false;
            madeItToExit = false;
            gameObject.SetActive(false);
        }

        if (dead == true)
        {
            enemyKilled?.Invoke(goldValue);
            enemyDeathSound?.Invoke(name);
            gameObject.SetActive(false);
            dead = false;
        }
    }

    private float ChangeHpBar()
    {
        float result = (float)hp / startHp; // Behövde casta till en float
        return result;
    }

    public void LooseHP(int dmgTaken)
    {
        hp -= dmgTaken;
        hpBackground.enabled = true;
        healthbar.enabled = true;
        healthbar.fillAmount = ChangeHpBar();
        if (hp <= 0)
        {
            dead = true;
            Debug.Log(name + " died");
            healthbar.enabled = false;
            hpBackground.enabled = false;
        }
    }
}
