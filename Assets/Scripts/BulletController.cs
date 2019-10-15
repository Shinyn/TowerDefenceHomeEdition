using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    private void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        
    }

    public void Shoot()
    {
        rigidbody.AddForce(Vector2.down * 1000);
    }
}
