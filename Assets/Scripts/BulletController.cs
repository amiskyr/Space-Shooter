using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody rb;

    public int damage = 1;
    public float movementSpeed;
    public WeaponUser shotBy;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    public void MoveBullet(WeaponUser user)
    {
        shotBy = user;
        if (shotBy == WeaponUser.Player)
        {
            rb.velocity = transform.forward * movementSpeed;
        }
        else
        {
            rb.velocity = transform.forward * (movementSpeed);
        }
    }
}
