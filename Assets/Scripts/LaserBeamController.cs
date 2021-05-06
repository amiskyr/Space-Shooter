using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamController : MonoBehaviour
{
    public int damage = 10;
    public WeaponUser shotBy;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        PlayerController.onWeaponSwitch += VerifyActivity;
    }

    private void OnDisable()
    {
        PlayerController.onWeaponSwitch -= VerifyActivity;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void FireLaserAttack(WeaponUser attacker)
    {
        shotBy = attacker;
    }

    void VerifyActivity()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
