using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipMovementController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private bool velocityReset = false;

    public float movementSpeed = 1f;
    public float orbitRadius = 1f;
    public float revolutionSpeed = 1f;
    public float playerFollowingSpeed = 5f;

    public EnemyMovementType movementPattern;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if(movementPattern == EnemyMovementType.OrbitPlayer)
        {
            OrbitPlayer();
        }
        else if(movementPattern == EnemyMovementType.FollowPlayer)
        {
            FollowPlayer();
        }
        else
        {
            MoveEnemyShip();
        }
    }

    private void OnEnable()
    {
        MoveEnemyShip();
    }

    private void MoveEnemyShip()
    {
        rb.velocity = -transform.forward * movementSpeed;
    }

    private void OrbitPlayer()
    {
        if(Vector3.Distance(rb.position, player.transform.position) <= 0.5f)
        {
            if(rb.velocity!=Vector3.zero && !velocityReset)
            {
                rb.velocity = Vector3.zero;
                velocityReset = true;
            }

            Vector3 relativePos = rb.position - player.transform.position;
            Vector3 tangentialVelocity = Vector3.Cross(player.transform.up, relativePos);

            rb.velocity = tangentialVelocity * revolutionSpeed;
        }
    }

    private void FollowPlayer()
    {
        rb.velocity = player.transform.position * playerFollowingSpeed;
    }
}
