using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector3 spawnValue;
    private Vector2 screenBounds;
    private Camera mainCamera;

    public int hazardCount;
    public float spawnWait, startWait, wavePeriod;

    public List<string> enemyObjectTags;

    private void Awake()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    void Start()
    {
        Invoke("Waves", 1f);
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while(true)
        {
            for(int i=0; i<hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0f, screenBounds.y / 2);
                Quaternion spawnRotation = Quaternion.identity;
            
                GameObject hazard = ObjectPooler.Instance.GetPooledObject(enemyObjectTags[0], spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(wavePeriod); 
        }
    }

    public void Waves()
    {
        StartCoroutine("SpawnWaves");
    }
}
