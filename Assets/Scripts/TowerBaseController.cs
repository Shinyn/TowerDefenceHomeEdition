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
    bool maxLevelTower = false;
    public GameManager gameManager;
    int upgradePrice; // behöver sättas när torn väljs
    int towerLevel = 0;

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
            if (showingTowers == true) // ha en bool för maxLvlTorn?
                DisableTowerChoice();

            else
                EnableTowerChoice();
        }

        if (towerChosen == true && maxLevelTower == false)
        {
            if (gameManager.gold >= upgradePrice)
            {
                towerLevel++;
                if (towerLevel == 4)
                    maxLevelTower = true;
                
                
                // if (tornLvl == 4) - maxLevelTower == true;
            }
            // metod för varje tornUppgradering?
            // möjlighet att kolla uppgradering innan val?

            //möjlighet att uppgradera om guld finns - if(gold >= upgradeprice) upgradeTower(currentTower) - currentTower detta gameObject
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        if (fireCountdown <= 0)
        {
            ShootBullet();
            fireCountdown = 1f / fireRate; // infinite fireRate = 0f
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

    // Behöver ha en andra towerchoice-metod för att uppgradera tornen? (inte disabla dom vid första valet)
    /*
        Archer lvl 2: dmg: 7 - 11 speed: fast range+
        Archer lvl 3: dmg: 10 - 16 speed: fast range++
        Archer lvl 4a: dmg 13 - 19 speed: very fast range+++
        Archer lvl 4b: dmg 35 - 65 speed: slow range+++++

        Mage lvl 2: dmg: 23 - 43 speed: slow range+
        Mage lvl 3: dmg: 40 - 74 speed: slow range+++
        Mage lvl 4a: dmg: 76 - 140 speed: very slow range++++
        Mage lvl 4b: dmg: 42 - 78 speed: slow range++++

        Cannon lvl 2: dmg: 20 - 40 speed: very slow range same
        Cannon lvl 3: dmg: 30 - 60 speed: very slow range++
        Cannon lvl 4a: dmg: 50 - 100 speed: very slow range++
        Cannon lvl 4b: dmg: 60 - 110 speed: very slow range same

        Ballista lvl 2: dmg: 9 - 12 speed: + range+
        Ballista lvl 3: dmg: 18 - 30 speed: ++ range: ++
        Ballista lvl 4a: dmg: 36 - 54 speed: ++ range +++
        Ballista lvl 4b: dmg: 48 - 72 speed: +++ range ++
        asd
    */

    private void UpgradeTower()
    {
        if (currentTower == "ArcherTower")
        {
            switch (towerLevel)
            {
                // Torn är lvl 1
                case 1:
                    // ändra stats till lvl 2
                    break;
                // Torn är lvl 2
                case 2:
                    // ändra stats till lvl 3
                    break;
                // Torn är lvl 3 -- Får se om det blir ett val här mellan 4a och 4b eller inte
                case 3:
                    // ändra stats till lvl 4 / 4a / 4b
                    break;
            }
            // ändra alla stats 
        }
        else if (currentTower == "MageTower")
        {

        }
        else if (currentTower == "BallistaTower")
        {

        }
        else if (currentTower == "CannonTower")
        {

        }
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
        towerLevel++;
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
