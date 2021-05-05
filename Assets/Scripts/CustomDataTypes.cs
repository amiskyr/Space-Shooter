using UnityEngine;

public interface IWeapon
{
    float ShotRecoveryTime { get; set; }
    GameObject Bullet { get; set; }
    void Shoot();
}

public enum WeaponType
{
    SemiAutomatic,
    FullyAutomatic,
    BurstShot,
    SpreadShot,
    Laser,
    StandardMissile,
    HomingMissile
}
