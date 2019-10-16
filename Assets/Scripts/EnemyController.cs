using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject goalPost;
    FollowPathController pathController;

    int hp = 10;
    int lifeCost = 1;
    int goldValue = 5;
    bool madeItToExit = false;
    SpriteRenderer sprite;
    float time;
    float timeCheckDelay = 0.1f;
    bool dead = false;

    public delegate void EnemyEscaped(int lifeLost);
    public static event EnemyEscaped enemyEscaped;

    public delegate void EnemyKilled(int goldgained);
    public static event EnemyKilled enemyKilled;

    private void Start()
    {
        time = Time.time;
        sprite = gameObject.GetComponent<SpriteRenderer>();
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
            dead = false;
            time = Time.time;
        }
    }

    public void TookDamage()
    {
        // if bullet hit - take dmg
        // if (dmgTaken > hp) {dead = true;}

    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        madeItToExit = true;
        dead = true;
    }

    // behöver metod för att välja fiender och kanske slumpa fram vilken som ska spawnas

    // Enemies-------------------------------------------------

    void Enemy1()
    {
        // Grunt
        hp = 10;
        lifeCost = 1;
        goldValue = 5;
        sprite.color = Color.red;
        pathController.moveSpeed = 5f;
    }

    void Enemy2()
    {
        // Hulk
        hp = 100;
        lifeCost = 3;
        goldValue = 80;
        sprite.color = Color.green;
        pathController.moveSpeed = 2f;
    }
}
