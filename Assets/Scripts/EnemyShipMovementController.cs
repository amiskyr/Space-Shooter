using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipMovementController : MonoBehaviour
{
    private Rigidbody rb;

    public float movementSpeed = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        MoveEnemyShip();
    }

    private void MoveEnemyShip()
    {
        rb.velocity = -transform.forward * movementSpeed;
    }
}
