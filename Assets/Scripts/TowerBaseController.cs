using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseController : MonoBehaviour
{
    public Transform target;
    float fireRate = 3f;
    float fireCountdown = 0f;
    float repeatRate = 0.25f;

    private bool showingTowers;
    SpriteRenderer baseColor;
    bool towerChosen = false;
    public GameObject bulletPrefab;
    bool detectedEnemy = false;
    public float detectRadius = 1.0f;
    float fireDelay = 0.1f;
    float lastTimeFired;
    private string enemiesTag = "Enemy";

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, repeatRate);
        lastTimeFired = Time.time;
        DisableTowerChoice();
        baseColor = gameObject.GetComponent<SpriteRenderer>();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemiesTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= detectRadius)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            else
            {
                target = null;
            }
        }

        if (nearestEnemy != null && shortestDistance <= detectRadius)
        {
            target = nearestEnemy.transform;
        }

    }

    private void OnMouseDown()
    {
        if (towerChosen == false)
        {
            if (showingTowers == true)
                DisableTowerChoice();

            else
                EnableTowerChoice();
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        if (fireCountdown <= 0)
        {
            ShootBullet();
            fireCountdown = 1f / fireRate; // infinite fireRate just nu
        }

        fireCountdown -= Time.deltaTime;

        /*
        DetectEnemies();
        if (detectedEnemy == true && Time.time > lastTimeFired + fireDelay)
        {
            ShootBullet();
            lastTimeFired = Time.time;
        } */

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    private void DisableTowerChoice()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        showingTowers = false;
    }

    private void EnableTowerChoice()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        showingTowers = true;
    }

    public void ChangeColorToTowerSelected(GameObject selectedTower)
    {
        baseColor.color = selectedTower.GetComponent<SpriteRenderer>().color; //tag
        towerChosen = true;
        DisableTowerChoice();
    }

    
    public void DetectEnemies()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius);
        if (hit != null && hit.tag == "Enemy")
        {
            detectedEnemy = true;
        }
        else
        {
            detectedEnemy = false;
        }
    }

    public void ShootBullet()
    {
        //Debug.Log("Shoot");
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        bulletController.TrackTarget(target);
        
    }
    // Skicka med vilket torn - ta färgen?

}
