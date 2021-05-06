using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public Transform weaponHolderXLeft;
    public Transform weaponHolderLeft;
    public Transform weaponHolderMid;
    public Transform weaponHolderRight;
    public Transform weaponHolderXRight;

    public float interval = 0.025f;

    public WeaponUser user;
    public float bulletRecoveryTime;
    public string bulletTag;
    
    private Vector2 screenBounds;
    private float nextFire;

    private Quaternion tiltedXL, tiltedL, tiltedR, tiltedXR;

    private void Awake()
    {
        tiltedXL = GenerateNewQuaternion(weaponHolderXLeft.rotation, 0f, -30f, 0f);
        tiltedL = GenerateNewQuaternion(weaponHolderLeft.rotation, 0f, -15f, 0f);
        tiltedR = GenerateNewQuaternion(weaponHolderRight.rotation, 0f, 15f, 0f);
        tiltedXR = GenerateNewQuaternion(weaponHolderXRight.rotation, 0f, 30f, 0f);
    }

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    public void ShootOnClick(WeaponType currentWeapon)
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            switch (currentWeapon)
            {
                case WeaponType.SingleShot:
                    SingleShotAttack();
                    break;
                case WeaponType.TripleShot:
                    TripleShotAttack();
                    break;
                case WeaponType.PentaShot:
                    PentaShotAttack();
                    break;
                case WeaponType.SpreadShot:
                    SpreadShotAttack();
                    break;
                case WeaponType.StraightMissile:
                    StraightMissileAttack();
                    break;
                case WeaponType.HomingMissile:
                    HomingMissileAttack();
                    break;
                case WeaponType.Laser:
                    LaserAttack();
                    break;
                default:
                    SingleShotAttack();
                    break;
            }

        }
    }
    public void ShootAutomatically()
    {
        if (transform.position.z <= -screenBounds.y/2)
        {
            return;
        }

        nextFire = Time.time + bulletRecoveryTime;
        GameObject bullet = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderMid.position, weaponHolderMid.rotation);
        bullet.GetComponent<BulletController>().MoveBullet(WeaponUser.Enemy);
    }

    public void SingleShotAttack()
    {
        nextFire = Time.time + bulletRecoveryTime;

        GameObject bullet = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderMid.position, weaponHolderMid.rotation);
        bullet.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
    }

    public void TripleShotAttack()
    {
        nextFire = Time.time + bulletRecoveryTime;

        GameObject bulletL = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderLeft.position, weaponHolderLeft.rotation);
        GameObject bulletM = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderMid.position, weaponHolderMid.rotation);
        GameObject bulletR = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderRight.position, weaponHolderRight.rotation);
        
        bulletL.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletM.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletR.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
    }

    public void PentaShotAttack()
    {
        nextFire = Time.time + bulletRecoveryTime;

        float offset = 0f;

        //offset = Mathf.Clamp(offset, 0.05f, 0.05f);

        //if(offset == 0.05f)
        //{
        //    interval = -1 * interval;
        //}
        //else if(offset == -0.05f)
        //{
        //    interval = -1 * interval;
        //}

        //offset += interval;
        Debug.Log(offset);

        Vector3 offsetPos = new Vector3(offset, 0f, 0f);

        GameObject bulletXL = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderXLeft.position + offsetPos, weaponHolderXLeft.rotation);
        GameObject bulletL = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderLeft.position + offsetPos, weaponHolderLeft.rotation);
        GameObject bulletM = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderMid.position + offsetPos, weaponHolderMid.rotation);
        GameObject bulletR = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderRight.position + offsetPos, weaponHolderRight.rotation);
        GameObject bulletXR = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderXRight.position + offsetPos, weaponHolderXRight.rotation);

        bulletXL.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletL.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletM.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletR.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletXR.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
    }

    public void SpreadShotAttack()
    {
        nextFire = Time.time + bulletRecoveryTime;
        
        GameObject bulletXL = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderXLeft.position, tiltedXL);
        GameObject bulletL = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderLeft.position, tiltedL);
        GameObject bulletM = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderMid.position, weaponHolderMid.rotation);
        GameObject bulletR = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderRight.position, tiltedR);
        GameObject bulletXR = ObjectPooler.Instance.GetPooledObject(bulletTag, weaponHolderXRight.position, tiltedXR);

        bulletXL.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletL.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletM.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletR.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
        bulletXR.GetComponent<BulletController>().MoveBullet(WeaponUser.Player);
    }

    public void StraightMissileAttack()
    {

    }

    public void HomingMissileAttack()
    {

    }

    public void LaserAttack()
    {

    }

    Quaternion GenerateNewQuaternion(Quaternion refRotation, float x, float y, float z)
    {
        Quaternion newRotation = new Quaternion();
        newRotation.eulerAngles = new Vector3(refRotation.eulerAngles.x + x, refRotation.eulerAngles.y + y, refRotation.eulerAngles.z + z);
        return newRotation;
    }
}
