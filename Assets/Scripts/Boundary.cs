using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        transform.localScale = new Vector3(screenBounds.x * 2, 0.5f, screenBounds.y * 2);
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
