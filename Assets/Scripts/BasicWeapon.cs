using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public Transform weaponHolder;
    //public GameObject bullet;
    public float fireRate;

    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
            if(bullet != null)
            {
                bullet.transform.position = weaponHolder.transform.position;
                bullet.transform.rotation = weaponHolder.transform.rotation;
                bullet.SetActive(true);
            }
        }
    }
}
