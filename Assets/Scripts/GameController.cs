using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Vector3 spawnValue;
    private Vector2 screenBounds;
    private Camera mainCamera;
    private PlayerController playerController;

    public Text scoreText;
    public Text highScoreText;
    public Text powerUpTImeText;
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
    public GameObject powerUp;
    public GameObject gameOverPanel;
    public Button replayButton;
    public Button exitButton;

    public static GameController Instance;
    [HideInInspector]
    public bool isGameOver = false;

    private void Awake()
    {
        Time.timeScale = 1f;

        Instance = this;

        mainCamera = Camera.main;
        playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();

        exitButton.onClick.AddListener(ExitGame);
        replayButton.onClick.AddListener(ReplayGame);
    }

    void Start()
    {
        if(!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Invoke("Waves", 1f);
    }

    private void Update()
    {
        if(Random.Range(0, 1000) == 28 && isGameOver == false && playerController.bulletsPowerUp == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), 0f, screenBounds.y + 0.25f);
            Instantiate(powerUp, spawnPosition, powerUp.transform.rotation);
        }
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

    public void GameOverPrompt()
    {
        Invoke("GameOver", 2f);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        if(PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore")}";
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
