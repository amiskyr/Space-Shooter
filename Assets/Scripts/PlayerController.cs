using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public InputType inputType;
    public int health;

    private BasicWeapon weapon;
    private PlayerMovementController movementController;

    private GameController gameController;
    private AudioManager audioManager;
    private ParticleSystem playerExplosion;

    void Start()
    {
        weapon = GetComponent<BasicWeapon>();
        movementController = GetComponent<PlayerMovementController>();
        gameController = GameController.Instance;
        audioManager = AudioManager.Instance;
        
        playerExplosion = gameController.playerExplosion;
    }

    void Update()
    {
        movementController.ReceiveTouchInput();

        weapon.ShootOnClick();
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
            if(health < 0)
            {
                playerExplosion.transform.position = transform.position;
                playerExplosion.Play();

                audioManager.PlayAudio(1);

                gameObject.SetActive(false);
            }
            else
            {
                health -= other.gameObject.GetComponent<BulletController>().damage;
            }
            other.gameObject.SetActive(false);

        }
    }
}
