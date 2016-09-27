using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BWeaponBulletMachineGun : BWeapons {


    [Header("Specific")]

    [Tooltip("Sprite of the crosshair")]
    public Image crosshair;

    public RectTransform crosshairCanvas;

    [Tooltip("must be a child of the shooter. the bullet will be created and shot at the angle of this transform")]
    public Transform bulletSpawnVector;

    public BProjectile bullet;
    public Transform firework;

    public float bulletSpeed = 500;

    public float noise = 0.07f;

    public new void Start()
    {
        base.Start();
        print("weapon line machine gun start");
        this.player = this.GetComponent<BPlayer>();
    }
    public override void doShoot()
    {

        // TODO: make object pool
        var bullet = this.makeBullet(this.bulletSpawnVector.position, this.getBulletDirectionNoisy(this.bulletSpawnVector.forward));
        
        this.player.setAnimation(BPlayer.PacifixAnimState.HeadVulcan);

    }

    public Vector3 getBulletDirectionNoisy(Vector3 direction)
    {
        var noiseVector = Random.insideUnitSphere;
        return direction + (noiseVector * this.noise);
    }

    BProjectile makeBullet(Vector3 spawnPoint, Vector3 direction)
    {
        var bull = Instantiate(this.bullet, spawnPoint, Quaternion.LookRotation(direction)) as BProjectile;

        Physics.IgnoreCollision(bull.GetComponentInChildren<Collider>(), this.player.GetComponentInChildren<Collider>());
        bull.damage = this.damage;
        bull.firework = this.firework;
        var rb = bull.GetComponent<Rigidbody>();
        //rb.AddForce( direction * 100000, ForceMode.Acceleration);
        rb.velocity = direction * this.bulletSpeed;
        return bull;
    }

}
