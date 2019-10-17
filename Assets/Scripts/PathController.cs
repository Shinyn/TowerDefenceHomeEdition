using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public GameObject pathPointsPrefab;
    Transform pathPoints;
    int nextPathPoint;
    public float moveSpeed = 5f;

    // Start is called before the first frame update
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

    // Update is called once per frame
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
            if (nextPathPoint < 14)
                nextPathPoint++;

            else if (nextPathPoint == 14)
            {
                gameObject.SetActive(false);
                EnemyController enemyController = gameObject.GetComponent<EnemyController>();
                enemyController.madeItToExit = true;
            }
        }
    }
}
