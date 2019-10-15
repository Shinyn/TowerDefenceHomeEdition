using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject goalPost;
    FollowPathController pathController;

    int hp;
    int lifeCost = 1;
    int goldValue;
    bool madeItToExit = false;
    SpriteRenderer sprite;
    float timeCheck;
    float lastTimeCheck = 1.5f;

    public delegate void EnemyEscaped(int lifeLost);
    public static event EnemyEscaped enemyEscaped;

    public delegate void EnemyKilled(int goldgained);
    public static event EnemyKilled enemyKilled;

    private void Start()
    {
        timeCheck = Time.time;
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (madeItToExit == true && Time.time > timeCheck + lastTimeCheck)
        {
            if (enemyEscaped != null)
              enemyEscaped(lifeCost);

            madeItToExit = false;
            timeCheck = Time.time;
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        madeItToExit = true;
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
