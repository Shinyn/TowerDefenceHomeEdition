using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [Header("Attributes")]
    public float moveSpeed = 5f;

    [Header("Other")]
    public GameObject pathPointsPrefab;
    Transform pathPoints;
    int nextPathPoint;
    
    void Start()
    {
        if (pathPoints == null)
        {
            pathPoints = Instantiate(pathPointsPrefab).transform;
        }
        nextPathPoint = 0;
    }

    private void OnEnable()
    {
        nextPathPoint = 0;
    }

    void Update()
    {
        Vector2 current = transform.position;
        Vector2 target = pathPoints.GetChild(nextPathPoint).position;
        if (Vector2.Distance(current, target) > 0.1)
        {
            transform.position = Vector2.MoveTowards(current, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (nextPathPoint < 18)
                nextPathPoint++;

            else if (nextPathPoint == 18)
            {
                EnemyController enemyController = gameObject.GetComponent<EnemyController>();
                enemyController.madeItToExit = true;
            }
        }
    }
}
