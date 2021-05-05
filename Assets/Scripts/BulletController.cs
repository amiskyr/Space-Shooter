using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;

    public float movementSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.velocity = transform.forward * movementSpeed;
        //Invoke("DisableBullet", 5);
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    void DisableBullet()
    {
        if(gameObject.activeInHierarchy == true)
        {
            gameObject.SetActive(false);
        }
    }
}
