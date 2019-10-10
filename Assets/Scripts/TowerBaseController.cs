using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseController : MonoBehaviour
{
    private bool showingTowers;
    SpriteRenderer baseColor;
    bool towerChosen = false;
    public GameObject bulletPrefab;
    bool detectedEnemy = false;
    public float detectRadius = 1.0f;

    void Start()
    {
        
        DisableTowerChoice();
        baseColor = gameObject.GetComponent<SpriteRenderer>();
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
        DetectEnemies();
        if (detectedEnemy == true)
        {
            StartCoroutine(ShootBullet());
        }

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
        baseColor.color = selectedTower.GetComponent<SpriteRenderer>().color;
        towerChosen = true;
        DisableTowerChoice();
    }

    
    public void DetectEnemies()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius);
        if (hit != null && hit.tag == "Enemy")
        {
            detectedEnemy = true;
            //Debug.Log("detected enemy");
        }
        else
        {
            detectedEnemy = false;
            //Debug.Log("nothing");
        }
        // Behöver en spherecast
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.)
    }

    public IEnumerator ShootBullet()
    {
        //Debug.Log("shootBullet");
        float shootDelay = 2.0f;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.Shoot();
        yield return new WaitForSeconds(shootDelay);

    }
    // Skicka med vilket torn - ta färgen?
    // Olika detect range
    // Olika dmg
    // Olika kostnad
    // 

}
