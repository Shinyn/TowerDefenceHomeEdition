using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject goalPost;
    Vector2 currentPosition;
    Vector2 targetPosition;

    private void Start()
    {
        currentPosition = transform.position;
    }

    private void Update()
    {
        //transform.position = Vector2.MoveTowards()
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
