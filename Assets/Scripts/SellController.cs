using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellController : MonoBehaviour
{
    public TowerBaseController tbc;

    private void OnMouseDown()
    {
        tbc.SellTower();
        // Efter att man sålt ett torn och köpt ett nytt så tar det 2 click att öppna tornvalen igen - varför?
    }
}
