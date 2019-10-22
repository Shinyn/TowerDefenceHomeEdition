using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public GameManager gameManager;
    public TowerBaseController tbc;

    // Kan göra om dehär till en metod i TowerBaseController
    private void OnMouseDown()
    {
        if (tbc.towerChosen == true && gameManager.gold >= tbc.upgradePrice)
        {
            if (tbc.maxLevelTower == false)
            {
                tbc.towerLevel++;
                tbc.UpgradeTower();
                tbc.UpdateCostAndValue();
                gameManager.RemoveGold(tbc.upgradePrice);
                if (tbc.towerLevel == 4)
                    tbc.maxLevelTower = true;
            }
        }
    }
}
