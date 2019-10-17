using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform target;
    float velocity = 30f;

    public void TrackTarget(Transform theTarget)
    {
        target = theTarget;
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
        Destroy(gameObject);
    }
}
