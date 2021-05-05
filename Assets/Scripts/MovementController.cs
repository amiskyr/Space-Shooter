using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody rb;
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float xMin, xMax, zMin, zMax;

    private Collider col;
    private Camera cam;
    private Vector3 pos;

    private bool isMoving;
    private bool gameOver;

    public float movementSpeed = 1f;
    public float tilt;

    public InputType inputType;

    private void Awake()
    {
        mainCamera = Camera.main;
        cam = Camera.main;
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        xMin = -screenBounds.x;
        xMax = screenBounds.x;
        zMin = -screenBounds.y / 2;
        zMax = screenBounds.y / 4;// - (screenBounds.y / 4);
    }

    private void Update()
    {
        ReceiveTouchInput();
    }

    private void FixedUpdate()
    {
        if(inputType == InputType.Keyboard)
        {
            MoveByKey();
        }
        else
        {
            MoveByTouch();
        }
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

    private void MoveByTouch()
    {
        if (isMoving)
        {
            rb.MovePosition(pos);
            rb.velocity = pos;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void ReceiveTouchInput()
    {
        if(!gameOver)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(col.Raycast(ray, out hit, 100f))
            {
                isMoving = Input.GetMouseButton(0);
            }
        }
        if(isMoving)
        {
            pos.x = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
            pos.z = mainCamera.ScreenToWorldPoint(Input.mousePosition).z;
        }
    }
}
