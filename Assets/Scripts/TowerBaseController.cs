using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseController : MonoBehaviour
{
    private bool showingTowers;
    public List<Transform> towerPlacements;
    
    void Start()
    {

        DisableTowerChoice();
        //SpawnTowerBases();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (showingTowers == true)
            DisableTowerChoice();

        else
            EnableTowerChoice();
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


    private void SpawnTowerBases()
    {
        towerPlacements.Add(gameObject.transform);
    }
    // onStart disable alla tornval
    // onTouch enable alla tornval
    // när ett torn valts byt sprite till rätt torn, disable towerBaseController för det tornet
}
