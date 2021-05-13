using System;
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
    private int health;

    public int maxHealth = 100;
    public InputType inputType;

    public float powerUpTime = 15f;
    public bool bulletsPowerUp;
    public WeaponType obtainedPowerUp;

    public static event Action onWeaponSwitch;

    void Start()
    {
        weapon = GetComponent<WeaponHandler>();
        movementController = GetComponent<PlayerMovementController>();
        gameController = GameController.Instance;
        audioManager = AudioManager.Instance;
        playerExplosion = gameController.playerExplosion;
        health = maxHealth;
    }

    void Update()
    {
        if(bulletsPowerUp == true)
        {
            powerUpTime -= Time.deltaTime * 1;
            gameController.powerUpTImeText.text = $"Time Left: {powerUpTime.ToString("f0")}s ";
            if(powerUpTime <= 0)
            {
                onWeaponSwitch?.Invoke();
                bulletsPowerUp = false;
                gameController.powerUpTImeText.text = "Time Left: 0s";
                powerUpTime = 15f;
            }
        }

        movementController.ReceiveTouchInput();
        if(bulletsPowerUp)
        {
            onWeaponSwitch?.Invoke();
            weapon.ShootOnClick(obtainedPowerUp);
        }
        else
        {
            weapon.ShootOnClick(WeaponType.SingleShot);
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
            gameController.healthPointsText.text = $"HP: {health}";
            other.gameObject.SetActive(false);
        }
    }

    private void GameOverPrompt()
    {
        gameController.GameOverPrompt();
    }
}
