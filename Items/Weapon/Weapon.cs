
using UnityEngine;

public enum WeaponName { DefaultGun, RocketGun };

public abstract class Weapon : Item{

    public float shootTime;

    public  WeaponName weaponName;

  public  bool canShoot = true;

    protected Camera cam;
    [SerializeField] protected AudioSource shootSound;
    [SerializeField] protected ParticleSystem shootEffect;
 
}
