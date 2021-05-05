using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public Transform weaponHolder;
    public float fireRate;

    private float nextFire;

    void Start()
    {
        
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = weaponHolder.transform.position;
                bullet.transform.rotation = weaponHolder.transform.rotation;
                bullet.SetActive(true);
            }
        }
    }
}
