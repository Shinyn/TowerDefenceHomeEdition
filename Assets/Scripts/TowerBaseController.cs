using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseController : MonoBehaviour
{
    [Header("Attributes")]
    [Range(1f, 3f)]
    public float fireRate = 3f;
    private float detectRadius = 0f;

    [Header("Other")]
    public GameObject bulletPrefab;
    public BulletController bulletController;
    float fireCountdown = 0f;
    float repeatRate = 0.25f;
    Transform target;
    private bool showingTowers;
    SpriteRenderer baseColor;
    bool towerChosen = false;
    bool detectedEnemy = false;
    float fireDelay = 0.1f;
    float lastTimeFired;
    private string enemiesTag = "Enemy";
    string currentTower;

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

    public void ChangeToTowerSelected(GameObject selectedTower)
    {
        baseColor.color = selectedTower.GetComponent<SpriteRenderer>().color;
        if (selectedTower.tag == "ArcherTower")
        {
            bulletController.damage = Random.Range(4, 7); // behöver randomisera varje gång den skjuter
            fireRate = 2;
            detectRadius = 2f;
            currentTower = selectedTower.tag;
        }
        else if (selectedTower.tag == "MageTower")
        {
            bulletController.damage = Random.Range(9, 18);
            fireRate = 1;
            detectRadius = 1.5f;
            currentTower = selectedTower.tag;
        }
        else if (selectedTower.tag == "BallistaTower")
        {
            bulletController.damage = Random.Range(6, 14);
            fireRate = 1;
            detectRadius = 1.2f;
            currentTower = selectedTower.tag;
        }
        else if (selectedTower.tag == "CannonTower")
        {
            bulletController.damage = Random.Range(8, 16);
            fireRate = 1;
            detectRadius = 1.8f;
            currentTower = selectedTower.tag;
        }
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
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        switch (currentTower)
        {
            case "ArcherTower":
                bulletController.damage = Random.Range(4, 7);
                break;
            case "MageTower":
                bulletController.damage = Random.Range(9, 18);
                break;
            case "BallistaTower":
                bulletController.damage = Random.Range(6, 14);
                break;
            case "CannonTower":
                bulletController.damage = Random.Range(8, 16);
                break;
        }
        
        if (bulletController != null)
        bulletController.TrackTarget(target);
    }
}
