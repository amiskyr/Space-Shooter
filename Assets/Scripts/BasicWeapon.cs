using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public Transform weaponHolder;
    public float fireRate;
    public string bulletTag;

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
            GameObject bullet = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolder.position, weaponHolder.rotation);
            //if (bullet != null)
            //{
            //    bullet.transform.position = weaponHolder.transform.position;
            //    bullet.transform.rotation = weaponHolder.transform.rotation;
            //    bullet.SetActive(true);
            //}
        }
    }
}
