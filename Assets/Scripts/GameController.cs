using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Vector3 spawnValue;
    private Vector2 screenBounds;
    private Camera mainCamera;
    
    public Text scoretext;
    public int score;
    public float initialWait = 1f;

    [Header("Hazard wave parameters")]
    public bool spawnHazards = false;
    public float hazardWaveInterval = 5f;
    public float hazardSpawnInterval = 0.5f;
    public int hazardCount;

    [Header("Enemy eave parameters")]
    public bool spawnEnemies = false;
    public float enemyWaveInterval = 5f;
    public float enemySpawnInterval = 2.5f;
    public int enemyCount;

    [Header("Other game Elements")]
    public ParticleSystem generalExplosion;
    public ParticleSystem playerExplosion;

    public List<string> enemyObjectPoolTags;

    public static GameController Instance;

    private void Awake()
    {
        Instance = this;

        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    void Start()
    {
        Invoke("Waves", 1f);
    }

    private IEnumerator SpawnHazards(string hazardTag)
    {
        yield return new WaitForSeconds(initialWait);

        while(true)
        {
            for(int i=0; i<hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0f, screenBounds.y - 0.25f);
                Quaternion spawnRotation = Quaternion.identity;
            
                GameObject hazard = ObjectPooler.Instance.GetPooledObject(hazardTag, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(hazardSpawnInterval);
            }
            yield return new WaitForSeconds(hazardWaveInterval); 
        }
    }

    private IEnumerator SpawnEnemy1(string enemyTag)
    {
        yield return new WaitForSeconds(initialWait);

        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0f, screenBounds.y - 1f); ;
                Quaternion spawnRotation = Quaternion.identity;

                GameObject enemy = ObjectPooler.Instance.GetPooledObject(enemyTag, spawnPosition, spawnRotation);
             
                yield return new WaitForSeconds(enemySpawnInterval);
            }         
            yield return new WaitForSeconds(enemyWaveInterval);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialWait);

        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), 0f, screenBounds.y - 0.25f); ;
                Quaternion spawnRotation = Quaternion.identity;

                int indexValue = Random.Range(2, enemyObjectPoolTags.Count);

                GameObject enemy = ObjectPooler.Instance.GetPooledObject(enemyObjectPoolTags[indexValue], spawnPosition, spawnRotation);

                yield return new WaitForSeconds(enemySpawnInterval);
            }
            yield return new WaitForSeconds(enemyWaveInterval);//(Random.Range(0.5f, enemyWaveInterval));
        }
    }

    public void Waves()
    {
        if(spawnHazards)
        {
            // Asteroids
            
            StartCoroutine(SpawnHazards(enemyObjectPoolTags[0]));   
        }
        if(spawnEnemies)
        {
            // Enemies

            StartCoroutine(SpawnEnemy1(enemyObjectPoolTags[1]));

            StartCoroutine(SpawnEnemies());
        }
    }
}
