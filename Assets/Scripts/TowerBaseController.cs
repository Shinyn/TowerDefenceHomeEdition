﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseController : MonoBehaviour
{
    private bool showingTowers;
    SpriteRenderer baseColor;
    bool towerChosen = false;
    public GameObject bulletPrefab;
    float bulletDelay = 1.0f;

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
        // if (enemy detected) {shoot at enemy}
      
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

    public float detectRadius = 1.0f;
    public void DetectEnemies()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius);
        if (hit != null && hit.tag == "Enemy")
        {
            StartCoroutine(ShootBullet());
        }
        // Behöver en spherecast
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.)
    }

    public IEnumerator ShootBullet()
    {
        GameObject bullet =  Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        bulletController.Shoot();

        yield return new WaitForSeconds(bulletDelay);
    }
    // Skicka med vilket torn - ta färgen?
    // Olika detect range
    // Olika dmg
    // Olika kostnad
    // 

}
