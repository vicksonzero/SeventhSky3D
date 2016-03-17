using UnityEngine;
using System.Collections;

public class BWeaponProjectile : BWeapons
{

    [Header("Specific")]
    public Rigidbody bullet;
    public float bulletSpeed = 100;
    [Tooltip("must be a child of the shooter. the bullet will be created and shot at the angle of this transform")]
    public Transform bulletSpawnVector;

    public override void doShoot()
    {
        Rigidbody bull = Instantiate(this.bullet, this.bulletSpawnVector.position, this.bulletSpawnVector.rotation) as Rigidbody;
        BPlayer player = this.GetComponent<BPlayer>();
        bull.velocity = this.bulletSpawnVector.forward * (this.GetComponent<Rigidbody>().velocity.magnitude + this.bulletSpeed);
    }

}
