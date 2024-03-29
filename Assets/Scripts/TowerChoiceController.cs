﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerChoiceController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public int price;
    public TowerBaseController towerBaseController;
    public GameManager gameManager;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        if (gameManager.gold >= price)
        {
            gameManager.RemoveGold(price);
            BuildTower();
        }
    }

    // onMouseDown så byt sprite på tornet
    // klicka igen för att bygga torn --- Måste kolla kostnad och om man har råd med tornet
    // när tornet är byggt så kan man inte bygga ett nytt
    // ska kunna sälja torn?

    // TextMesh PRO som underObject till varje towerChoice - ta price från parent och visa ovanför

    public void BuildTower()
    {
        towerBaseController.ChangeToTowerSelected(gameObject);
    }
}
