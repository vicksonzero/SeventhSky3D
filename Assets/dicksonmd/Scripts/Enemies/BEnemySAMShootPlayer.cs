using UnityEngine;
using System.Collections;

public class BEnemySAMShootPlayer : MonoBehaviour {

    public float repeatInterval = 3;
    public float repeatIntervalNoise = 1;

    public float minDistance = 300;

    public float cooldown = 0.3f;

    public float damage = 5;
    public float bulletSpeed = 400;

    public Transform bulletSpawnVector;
    public BEnemyProjectile bullet;
    public Transform firework;

    private BGameMaster game;
    private float minDistanceSq;

    private Vector3 aim;
    private Vector3 newAim;

    // Use this for initialization
    void Start () {
        this.game = GameObject.FindObjectOfType<BGameMaster>();
        StartCoroutine("updateWanderDirection");
        this.minDistanceSq = this.minDistance * this.minDistance;

        var player = this.game.player;
        this.aim = player.transform.position - this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	}

    IEnumerator updateWanderDirection()
    {
        while (true)
        {
            var player = this.game.player;
            this.newAim = player.transform.position - this.transform.position;
            if(this.newAim.sqrMagnitude < this.minDistanceSq)
            {
                print("Enemy shoot");
                StartCoroutine("doShoot");
            }
            yield return new WaitForSeconds(this.repeatInterval + Random.Range(0, this.repeatIntervalNoise));
        }
    }

    IEnumerator doShoot()
    {
        var player = this.game.player;
        this.newAim = player.transform.position - this.transform.position;
        var directionVector = Vector3.Slerp(this.aim, this.newAim, 0.8f);
        var bullet = this.makeBullet(this.bulletSpawnVector.position, directionVector);

        yield return new WaitForSeconds(this.cooldown);

        this.newAim = player.transform.position - this.transform.position;
        directionVector = Vector3.Slerp(this.aim, this.newAim, 0.9f);
        bullet = this.makeBullet(this.bulletSpawnVector.position, directionVector);

        yield return new WaitForSeconds(this.cooldown);

        this.newAim = player.transform.position - this.transform.position;
        directionVector = Vector3.Slerp(this.aim, this.newAim, 1);
        bullet = this.makeBullet(this.bulletSpawnVector.position, directionVector);

        yield return new WaitForSeconds(this.cooldown);

        this.newAim = player.transform.position - this.transform.position;
        directionVector = Vector3.Slerp(this.aim, this.newAim, 1.1f);
        bullet = this.makeBullet(this.bulletSpawnVector.position, directionVector);

        yield return new WaitForSeconds(this.cooldown);

        this.newAim = player.transform.position - this.transform.position;
        directionVector = Vector3.Slerp(this.aim, this.newAim, 1.2f);
        bullet = this.makeBullet(this.bulletSpawnVector.position, directionVector);

        this.aim = this.newAim;
    }


    BEnemyProjectile makeBullet(Vector3 spawnPoint, Vector3 direction)
    {
        var bull = Instantiate(this.bullet, spawnPoint, Quaternion.LookRotation(direction)) as BEnemyProjectile;

        Physics.IgnoreCollision(bull.GetComponentInChildren<Collider>(), this.GetComponentInChildren<Collider>());
        bull.damage = this.damage;
        bull.firework = this.firework;
        var rb = bull.GetComponent<Rigidbody>();
        //rb.AddForce( direction * 100000, ForceMode.Acceleration);
        rb.velocity = direction.normalized * this.bulletSpeed;
        return bull;
    }
    void OnDestroy()
    {
        this.StopAllCoroutines();
    }
}
