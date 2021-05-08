using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovementController : MonoBehaviour
{
    private Rigidbody rb;
    
    public float tumble = 1f;
    public float movementSpeed = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        rb.velocity = -transform.forward * movementSpeed;
    }
}
