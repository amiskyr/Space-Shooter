using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public Transform weaponHolder;
    public float bulletRecoveryTime;
    public string bulletTag;
    public WeaponUser user;
    
    private Vector2 screenBounds;
    private float nextFire;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void ShootOnClick()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + bulletRecoveryTime;
            GameObject bullet = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolder.position, weaponHolder.rotation);
            bullet.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        }
    }

    public void ShootAutomatically()
    {
        if (transform.position.z <= -screenBounds.y/2)
        {
            return;
        }

        nextFire = Time.time + bulletRecoveryTime;
        GameObject bullet = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolder.position, weaponHolder.rotation);
        bullet.GetComponent<BulletController>().MoveBullet(WeaponUser.Enemy);
    }
}
