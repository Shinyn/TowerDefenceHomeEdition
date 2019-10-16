using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform target;
    float velocity = 100f;
    private void Awake()
    {
        
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = target.position - transform.position;
        float distanceThisFrame = velocity * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            Impact();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

    }

    public void Impact()
    {
        Debug.Log("We made impact with target!");
        Destroy(gameObject);
    }


    public void TrackTarget(Transform theTarget)
    {
        theTarget = target;
    }
}
