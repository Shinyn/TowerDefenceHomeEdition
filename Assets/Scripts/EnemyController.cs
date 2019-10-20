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
    SpriteRenderer sprite;
    float time;
    float timeCheckDelay = 0.1f;
    bool dead = false;
    public int startHp;
    private bool neverUsed = true;
    public Image healthbar;

    public delegate void EnemyEscaped(int lifeLost);
    public static event EnemyEscaped enemyEscaped;

    public delegate void EnemyKilled(int goldgained);
    public static event EnemyKilled enemyKilled;

    /*
     Behöver reference till image hp, visa den när fienden blir attackerad första gången
         */
    private void Start()
    {
        time = Time.time;
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
      if (neverUsed)
            startHp = hp;
      else
            hp = startHp;
        healthbar.fillAmount = hp / startHp;
    }

    private void OnDisable()
    {
        neverUsed = false;
    }


    private void Update()
    {
        if (madeItToExit == true && Time.time > time + timeCheckDelay)
        {
            enemyEscaped?.Invoke(lifeCost);
            madeItToExit = false;
            time = Time.time;
        }

        if (dead == true && Time.time > time + timeCheckDelay)
        {
            enemyKilled?.Invoke(goldValue);
            gameObject.SetActive(false);
            dead = false;
            time = Time.time;
        }
    }

    public void ChangeGoldvalue(int gold)
    {
        goldValue = gold; // Använd tags, gör ett underobject med en tag och targeta sen parent
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void LooseHP(int dmgTaken)
    {
        hp -= dmgTaken;
        healthbar.fillAmount = hp / startHp;
        //Debug.Log("Hp is: " + hp);
        if (hp <= 0)
        {
            dead = true;
        }
    }
}
