using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float xMin, xMax, zMin, zMax;

    public float movementSpeed = 1f;
    public float tilt;

    private void Awake()
    {
        mainCamera = Camera.main;
        
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        xMin = -screenBounds.x;
        xMax = screenBounds.x;
        zMin = -screenBounds.y / 2;
        zMax = screenBounds.y / 4;// - (screenBounds.y / 4);
    }

    private void FixedUpdate()
    {
        MoveByKey();
    }

    private void MoveByKey()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * movementSpeed;

        rb.position = new Vector3(Mathf.Clamp(rb.position.x, xMin, xMax), 0f, Mathf.Clamp(rb.position.z, zMin, zMax));

        rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
    }
}
