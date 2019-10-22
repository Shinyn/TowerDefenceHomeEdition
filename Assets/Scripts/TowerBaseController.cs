using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseController : MonoBehaviour
{
    [Header("Attributes")]
    [Range(1f, 3f)]
    public float fireRate = 3f;
    private float detectRadius = 0f; // Ändra med 0.5 intervall

    [Header("Other")]
    public GameObject bulletPrefab;
    public BulletController bulletController;
    public GameManager gameManager;
    float fireCountdown = 0f;
    float repeatRate = 0.25f;
    Transform target;
    SpriteRenderer baseColor;
    bool detectedEnemy = false;
    float fireDelay = 0.1f;
    float lastTimeFired;
    private string enemiesTag = "Enemy";
    public TowerChoiceController tcc;
    public TowerChoiceController tcc1;
    public TowerChoiceController tcc2;
    public TowerChoiceController tcc3;

    // Tower variables
    bool towerChosen = false;
    private bool showingTowers;
    string currentTower;
    bool maxLevelTower = false;
    int upgradePrice;
    int towerLevel = 0;
    [SerializeField]
    int towerValue = 0;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, repeatRate);
        lastTimeFired = Time.time;
        DisableTowerChoice();
        baseColor = gameObject.GetComponent<SpriteRenderer>();
        //tcc = gameObject.GetComponentInChildren<TowerChoiceController>(); // Har 4 tcc så den vet inte vilken den ska ta och blir null?
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

    private void SellTower() { // Gör ett eget lager för sell, upgrade och base tower
        int cashBack = Mathf.RoundToInt(towerValue * 0.9f); // GLÖMDE ETT F!!! :@
        gameManager.AddGold(cashBack);
        towerChosen = false;
        detectRadius = 0;
        BulletDamage(0, 0);
        towerLevel = 0;
        towerValue = 0;
        baseColor.color = Color.white;
        // Disabla torn specialiteter om dom har några
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

        if (towerChosen == true && maxLevelTower == false)
        {
            
            //Debug.Log("upgradePrice is: " + upgradePrice);
            // Måste ha in goldCost Ovanför tornet och om man klickar så man ser kostnaden
            if (gameManager.gold >= upgradePrice)
            {
                towerLevel++;
                //Debug.Log("now towerLevel is: " + towerLevel);
                if (towerLevel == 4)
                    maxLevelTower = true;
                UpgradeTower();
                gameManager.RemoveGold(upgradePrice);
                // ta bort guld kostnaden
            }
            // möjlighet att kolla uppgradering innan val?
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
    */
    private void BulletDamage(int minDmg, int maxDmg)
    {
        bulletController.damage = Random.Range(minDmg, maxDmg);
    }

    private void UpgradeTower()
    {
        if (currentTower == "ArcherTower")
        {
            switch (towerLevel)
            {
                // Torn är lvl 1 -> ändra stats till lvl 2
                case 2:
                    towerValue += upgradePrice;
                    upgradePrice = 160;
                    BulletDamage(7, 12);
                    fireRate = 1.5f;
                    detectRadius = 2f;
                    break;
                // Torn är lvl 2 -> ändra stats till lvl 3
                case 3:
                    towerValue += upgradePrice;
                    upgradePrice = 230;
                    BulletDamage(10, 17);
                    fireRate = 2f;
                    detectRadius = 2.5f;
                    break;
                // Torn är lvl 3 -> ändra stats till lvl 4 / 4a / 4b -- Får se om det blir ett val här mellan 4a och 4b eller inte
                case 4:
                    BulletDamage(13, 20);
                    fireRate = 3f;
                    detectRadius = 3f;
                    towerValue += upgradePrice;
                    break;
            }
        }
        else if (currentTower == "MageTower") // Vill fixa magic resist och armor så torn-typerna spelar större roll
        {
            switch (towerLevel)
            {
                // Torn är lvl 1 -> ändra stats till lvl 2
                case 2:
                    towerValue += upgradePrice;
                    upgradePrice = 216;
                    BulletDamage(23, 44);
                    fireRate = 1.3f;
                    detectRadius = 1.8f;
                    break;
                // Torn är lvl 2 -> ändra stats till lvl 3
                case 3:
                    towerValue += upgradePrice;
                    upgradePrice = 270;
                    BulletDamage(40, 75);
                    fireRate = 1.6f;
                    detectRadius = 2.2f;
                    break;
                // Torn är lvl 3 -> ändra stats till lvl 4 / 4a / 4b -- Får se om det blir ett val här mellan 4a och 4b eller inte
                case 4:
                    BulletDamage(76, 141);
                    fireRate = 1.8f;
                    detectRadius = 2.5f;
                    towerValue += upgradePrice;
                    break;
            }
        }
        else if (currentTower == "BallistaTower")
        {
            switch (towerLevel)
            {
                // Torn är lvl 1 -> ändra stats till lvl 2
                case 2:
                    towerValue += upgradePrice;
                    upgradePrice = 160;
                    BulletDamage(9, 13);
                    fireRate = 1.5f;
                    detectRadius = 1.8f;
                    break;
                // Torn är lvl 2 -> ändra stats till lvl 3
                case 3:
                    towerValue += upgradePrice;
                    upgradePrice = 230;
                    BulletDamage(18, 31);
                    fireRate = 1.8f;
                    detectRadius = 2.2f;
                    break;
                // Torn är lvl 3 -> ändra stats till lvl 4 / 4a / 4b -- Får se om det blir ett val här mellan 4a och 4b eller inte
                case 4:
                    BulletDamage(48, 73);
                    fireRate = 2f;
                    detectRadius = 2.5f;
                    towerValue += upgradePrice;
                    break;
            }
        }
        else if (currentTower == "CannonTower")
        {
            switch (towerLevel)
            {
                // Torn är lvl 1 -> ändra stats till lvl 2
                case 2:
                    towerValue += upgradePrice;
                    upgradePrice = 288;
                    BulletDamage(20, 41);
                    fireRate = 1.5f;
                    break;
                // Torn är lvl 2 -> ändra stats till lvl 3
                case 3:
                    towerValue += upgradePrice;
                    upgradePrice = 337;
                    BulletDamage(30, 61);
                    detectRadius = 2f;
                    break;
                // Torn är lvl 3 -> ändra stats till lvl 4 / 4a / 4b -- Får se om det blir ett val här mellan 4a och 4b eller inte
                case 4:
                    BulletDamage(60, 101);
                    fireRate = 2f;
                    detectRadius = 2.5f;
                    towerValue += upgradePrice;
                    break;
            }
        }
    }
    

    public void ChangeToTowerSelected(GameObject selectedTower)
    {
        baseColor.color = selectedTower.GetComponent<SpriteRenderer>().color;
        if (selectedTower.tag == "ArcherTower")
        {
            BulletDamage(4, 7);
            fireRate = 1;
            detectRadius = 1.5f;
            currentTower = selectedTower.tag;
            upgradePrice = 110;
            towerValue = tcc.price;
            Debug.Log("tcc value: " + tcc.price);
        }
        else if (selectedTower.tag == "MageTower")
        {
            BulletDamage(9, 18);
            fireRate = 1;
            detectRadius = 1.5f;
            currentTower = selectedTower.tag;
            upgradePrice = 144;
            towerValue = tcc1.price;
            Debug.Log("tcc1 value: " + tcc1.price);
        }
        else if (selectedTower.tag == "BallistaTower")
        {
            BulletDamage(6, 14);
            fireRate = 1;
            detectRadius = 1.5f;
            currentTower = selectedTower.tag;
            upgradePrice = 110;
            towerValue = tcc2.price;
            Debug.Log("tcc2 value: " + tcc2.price);
        }
        else if (selectedTower.tag == "CannonTower")
        {
            BulletDamage(8, 16);
            fireRate = 1;
            detectRadius = 1.5f;
            currentTower = selectedTower.tag;
            upgradePrice = 198;
            towerValue = tcc3.price;
            Debug.Log("tcc3 value: " + tcc3.price);
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
            detectedEnemy = true; // Bra ställe att lägga en dot för något torn :D
            // enemyController.LooseHP(dotDamage); varje 0.2 sekunder eller så
        }
        else
        {
            detectedEnemy = false;
        }
    }

    public void ShootBullet()
    {
        // instansiera olika bullets?
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        switch (currentTower)
        {
            case "ArcherTower":
                switch (towerLevel)
                {
                    case 1:
                        BulletDamage(4, 7);
                        break;
                    case 2:
                        BulletDamage(7, 12);
                        break;
                    case 3:
                        BulletDamage(10, 17);
                        break;
                    case 4:
                        BulletDamage(16, 26);
                        break;
                }
                break;
            case "MageTower":
                switch (towerLevel)
                {
                    case 1:
                        BulletDamage(9, 18);
                        break;
                    case 2:
                        BulletDamage(23, 44);
                        break;
                    case 3:
                        BulletDamage(40, 75);
                        break;
                    case 4:
                        BulletDamage(76, 141);
                        break;
                }
                break;
            case "BallistaTower":
                switch (towerLevel)
                {
                    case 1:
                        BulletDamage(6, 13);
                        break;
                    case 2:
                        BulletDamage(18, 31);
                        break;
                    case 3:
                        BulletDamage(36, 55);
                        break;
                    case 4:
                        BulletDamage(48, 73);
                        break;
                }
                break;
            case "CannonTower":
                switch (towerLevel)
                {
                    case 1:
                        BulletDamage(8, 16);
                        break;
                    case 2:
                        BulletDamage(20, 41);
                        break;
                    case 3:
                        BulletDamage(30, 61);
                        break;
                    case 4:
                        BulletDamage(60, 111);
                        break;
                }
                break;
        }
        
        if (bulletController != null)
        bulletController.TrackTarget(target);
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
}
