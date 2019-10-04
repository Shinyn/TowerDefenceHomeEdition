using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerChoiceController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public TowerBaseController towerBaseController;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        //string towerColor = spriteRenderer.color.ToString();
        //Debug.Log(towerColor);
        BuildTower();
    }

    // onMouseDown så byt sprite på tornet
    // klicka igen för att bygga torn
    // när tornet är byggt så kan man inte bygga ett nytt
    // ska kunna sälja torn?

    public void BuildTower()
    {
        towerBaseController.ChangeColorToTowerSelected(gameObject);
    }
}
