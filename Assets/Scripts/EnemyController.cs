using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public delegate void EnemyEscaped(int lifeLost);
    public static event EnemyEscaped enemyEscaped;

    public delegate void EnemyKilled(int goldgained);
    public static event EnemyKilled enemyKilled;

    private void Start()
    {
        //startHp = hp;
        time = Time.time;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("OUCH!!!");
    }

    public void LooseHP(int dmgTaken)
    {
        hp -= dmgTaken;
        if (hp <= 0)
        {
            dead = true;
        }
        //Debug.Log("hp = " + hp);
        // if bullet hit - take dmg
        // if (dmgTaken > hp) {dead = true;}
    }

    private void OnBecameInvisible()
    {
        //gameObject.SetActive(false);
        //madeItToExit = true;
        //dead = true;
    }
}
