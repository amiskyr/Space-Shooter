using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody rb;
    public int damage = 1;
    public WeaponUser shotBy;
    public float movementSpeed;

    [Header("Homing projectiles")]
    public bool isHoming = false;
    public float rotateSpeed;
    public float detectionRange = 1f;

    private Transform targetTransform;
    private bool targetFound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (isHoming && !targetFound)
        {
            LocateTarget();
        }
        if (isHoming && targetFound)
        {
            FollowTarget();
        }
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
            rb.velocity = transform.forward * movementSpeed;
        }
    }

    public void FollowTarget()
    {
        Vector3 direction = targetTransform.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.forward).y;

        rb.angularVelocity = new Vector3(0f, -rotateAmount * rotateSpeed, 0f);

        rb.velocity = transform.forward * movementSpeed;
    }

    public void LocateTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);

        for(int i=0; i<colliders.Length; i++)
        {
            if(colliders[i].gameObject.tag.Equals("Enemy"))
            {
                targetTransform = colliders[i].gameObject.transform;
                targetFound = true;
            }
        }
    }
}
