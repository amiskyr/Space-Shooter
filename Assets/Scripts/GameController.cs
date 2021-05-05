using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector3 spawnValue;
    private Vector2 screenBounds;
    private Camera mainCamera;

    public List<string> enemyObjectTags;

    private void Awake()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    void Start()
    {
        SpawnWaves();
    }

    private void SpawnWaves()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0f, screenBounds.y / 2);
        Quaternion spawnRotation = Quaternion.identity;

        GameObject hazard = ObjectPooler.Instance.GetPooledObject("Asteroid", spawnPosition, spawnRotation);
    }
}
