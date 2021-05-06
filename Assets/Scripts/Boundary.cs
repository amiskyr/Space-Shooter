using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private Camera mainCamera;
    
    [HideInInspector]
    public Vector2 screenBounds;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        transform.localScale = new Vector3(screenBounds.x * 2, 0.1f, screenBounds.y * 1);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
