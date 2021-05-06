using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private ParticleSystem generalexplosion;
    private ParticleSystem playerExplosion;
    private GameController gameController;
    private AudioManager audioManager;
    private BasicWeapon weapon;

    public int health;
    public float shootingInterval;
    public EnemyType enemyType;

    private void Awake()
    {
        gameController = GameController.Instance;
        audioManager = AudioManager.Instance;
        generalexplosion = gameController.generalExplosion;
        playerExplosion = gameController.playerExplosion;
    }

    private void OnEnable()
    {
        if (enemyType == EnemyType.SpaceShip)
        {
            weapon = GetComponent<BasicWeapon>();
            InvokeRepeating("FireWeapon", weapon.bulletRecoveryTime, shootingInterval);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Bullet") && other.gameObject.GetComponent<BulletController>().shotBy == WeaponUser.Player)
        {
            if(health <= 0)
            {
                generalexplosion.transform.position = transform.position;
                generalexplosion.Play();

                UpdateScore();

                audioManager.PlayAudio(0);
                
                gameObject.SetActive(false);
            }
            else
            {
                health -= other.GetComponent<BulletController>().damage;
            }
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.tag.Equals("Player"))
        {
            gameController.isGameOver = true;
            playerExplosion.transform.position = other.transform.position;
            playerExplosion.Play();

            other.gameObject.SetActive(false);
            
            generalexplosion.transform.position = transform.position;
            generalexplosion.Play();
            
            audioManager.PlayAudio(0);

            GameOverPrompt();

            gameObject.SetActive(false);
        }
    }

    private void UpdateScore()
    {
        gameController.score += 10;
        gameController.scoreText.text = $"Score: {gameController.score}";
    }

    private void FireWeapon()
    {
        if (enemyType == EnemyType.SpaceShip)
        {
            weapon.ShootAutomatically();
        }
    }

    private void GameOverPrompt()
    {
        gameController.GameOverPrompt();
    }
}
