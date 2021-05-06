using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private WeaponHandler weapon;
    private PlayerMovementController movementController;

    private GameController gameController;
    private AudioManager audioManager;
    private ParticleSystem playerExplosion;

    public InputType inputType;
    public int health;

    public bool bulletsPowerUp;
    public float powerUpTime = 10f;
    public WeaponType obtainedPowerUp;

    void Start()
    {
        weapon = GetComponent<WeaponHandler>();
        movementController = GetComponent<PlayerMovementController>();
        gameController = GameController.Instance;
        audioManager = AudioManager.Instance;
        
        playerExplosion = gameController.playerExplosion;
    }

    void Update()
    {
        if(bulletsPowerUp == true)
        {
            powerUpTime -= Time.deltaTime * 1;
            gameController.powerUpTImeText.text = $"Time Left: {powerUpTime.ToString("f0")}s ";
            if(powerUpTime <= 0)
            {
                bulletsPowerUp = false;
                gameController.powerUpTImeText.text = "Time Left: 0s";
                powerUpTime = 15f;
            }
        }

        movementController.ReceiveTouchInput();
        if(bulletsPowerUp)
        {
            weapon.ShootOnClick(obtainedPowerUp);
        }
        else
        {
            weapon.ShootOnClick(WeaponType.Laser);
        }
    }

    private void FixedUpdate()
    {
        if (inputType == InputType.Keyboard)
        {
            movementController.MoveByKey();
        }
        else
        {
            movementController.MoveByTouch();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Bullet") && other.gameObject.GetComponent<BulletController>().shotBy == WeaponUser.Enemy)
        {
            if(health <= 0)
            {
                gameController.isGameOver = true;

                playerExplosion.transform.position = transform.position;
                playerExplosion.Play();

                audioManager.PlayAudio(1);

                GameOverPrompt();

                gameObject.SetActive(false);
            }
            else
            {
                health -= other.gameObject.GetComponent<BulletController>().damage;
            }
            other.gameObject.SetActive(false);
        }
    }

    private void GameOverPrompt()
    {
        gameController.GameOverPrompt();
    }
}
