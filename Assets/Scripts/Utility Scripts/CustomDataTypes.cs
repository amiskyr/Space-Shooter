using UnityEngine;

public interface IWeapon
{
    float ShotRecoveryTime { get; set; }
    GameObject Bullet { get; set; }
    void Shoot();
}

public enum InputType
{
    Touch,
    Keyboard
}

public enum EnemyType
{
    Hazard,
    SpaceShip,
    Boss
}

public enum WeaponUser
{
    Enemy,
    Player
}

public enum WeaponType
{
    SingleShot,
    TripleShot,
    PentaShot,
    SpreadShot,
    Torpedo,
    Missile,
    Laser
}

public enum EnemyMovementType
{
    FollowPlayer,
    OrbitPlayer,
    MoveForward
}