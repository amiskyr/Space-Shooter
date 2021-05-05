using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    private float shotRecoveryTime;
    public float ShotRecoveryTime 
    { 
        get => shotRecoveryTime; 
        set => shotRecoveryTime = value; 
    }

    private GameObject bullet;
    public GameObject Bullet 
    { 
        get => bullet; 
        set => bullet = value; 
    }

    public void Shoot()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
