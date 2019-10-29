using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitController : MonoBehaviour
{
    private void OnMouseDown()
    {
        Application.Quit();
        Debug.Log("Quit From Pause");
    }
}
