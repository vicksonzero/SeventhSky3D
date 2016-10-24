using UnityEngine;
using System.Collections;
using System.Linq;

public class BEnemySAMShootPlayer : MonoBehaviour
{

    public float repeatInterval = 3;
    public float repeatIntervalNoise = 1;

    public float minDistance = 300;

    public float cooldown = 0.3f;

    public float damage = 5;
    public float bulletSpeed = 400;

    public Transform[] bulletSpawnVectors;
    public BEnemyMissile bullet;
    public Transform firework;

    private BGameMaster game;
    private float minDistanceSq;

    private Vector3 aim;
    private Vector3 newAim;

    private BEnemyMissile bulletGO;

    // Use this for initialization
    void Start()
    {
        this.game = GameObject.FindObjectOfType<BGameMaster>();
        StartCoroutine("updateWanderDirection");
        this.minDistanceSq = this.minDistance * this.minDistance;

        var player = this.game.player;
        this.aim = player.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator updateWanderDirection()
    {
        while (true)
        {
            var player = this.game.player;
            this.newAim = player.transform.position - this.transform.position;
            if (this.newAim.sqrMagnitude < this.minDistanceSq)
            {
                print("Enemy shoot");
                StartCoroutine("doShoot");
            }
            yield return new WaitForSeconds(this.repeatInterval + Random.Range(0, this.repeatIntervalNoise));
        }
    }

    IEnumerator doShoot()
    {
        // if bullet has disappeared
        if (bulletGO == null)
        {
            var bulletSpawnVectorIndex = Random.Range(0, this.bulletSpawnVectors.Length);
            var bulletSpawnVector = this.bulletSpawnVectors[bulletSpawnVectorIndex];

            var player = this.game.player;

            this.newAim = player.transform.position - this.transform.position;
            var directionVector = Vector3.Slerp(this.aim, this.newAim, 0.8f);
            var bullet = this.makeBullet(bulletSpawnVector.position, directionVector);
            this.bulletGO = bullet;

            yield return new WaitForSeconds(this.cooldown);

            this.aim = this.newAim;

        }
    }


    BEnemyMissile makeBullet(Vector3 spawnPoint, Vector3 direction)
    {
        var bull = Instantiate(this.bullet, spawnPoint, Quaternion.LookRotation(direction)) as BEnemyMissile;

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
