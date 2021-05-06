using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public WeaponType weaponPowerUp;

    void Start()
    {
        Invoke("HidePowerUp", 10f);
        weaponPowerUp = (WeaponType)Random.Range(0, 7);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            PlayerController tempPC = other.GetComponent<PlayerController>();
            tempPC.bulletsPowerUp = true;
            tempPC.obtainedPowerUp = weaponPowerUp;
            //gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }

    public void HidePowerUp()
    {
        gameObject.SetActive(false); 
    }
}
