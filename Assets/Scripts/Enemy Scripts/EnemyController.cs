using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private ParticleSystem generalExplosion;
    private ParticleSystem playerExplosion;
    private GameController gameController;
    private AudioManager audioManager;
    private WeaponHandler weapon;
    public int maxHealth = 10;

    private int health;
    public float shootingInterval;
    public EnemyType enemyType;

    public WeaponType currentWeapon;

    private void Awake()
    {
        gameController = GameController.Instance;
        audioManager = AudioManager.Instance;
        generalExplosion = gameController.generalExplosion;
        playerExplosion = gameController.playerExplosion;
    }

    private void OnEnable()
    {
        health = maxHealth;

        currentWeapon = (WeaponType)Random.Range(0, 4);

        if (enemyType == EnemyType.SpaceShip)
        {
            weapon = GetComponent<WeaponHandler>();
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
                Die();
            }
            else
            {
                health -= other.GetComponent<BulletController>().damage;
            }
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.tag.Equals("LaserBeam") && other.gameObject.GetComponent<LaserBeamController>().shotBy == WeaponUser.Player)
        {
            if (health <= 0)
            {
                Die();
            }
            else
            {
                health -= other.GetComponent<LaserBeamController>().damage;
            }
        }
        if(other.gameObject.tag.Equals("Player"))
        {
            gameController.isGameOver = true;
            playerExplosion.transform.position = other.transform.position;
            playerExplosion.Play();

            other.gameObject.SetActive(false);
            
            GameOverPrompt();
            
            generalExplosion.transform.position = transform.position;
            generalExplosion.Play();

            audioManager.PlayAudio(0);

            gameObject.SetActive(false);
        }
    }

    private void Die()
    {
        generalExplosion.transform.position = transform.position;
        generalExplosion.Play();

        UpdateScore();

        audioManager.PlayAudio(0);

        gameObject.SetActive(false);
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
            weapon.ShootAutomatically(currentWeapon);
        }
    }

    private void GameOverPrompt()
    {
        gameController.GameOverPrompt();
    }
}
