using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public GameManager gameManager;
    public TowerBaseController tbc;

    private void OnMouseDown()
    {
        tbc.Upgrade();
    }
}
