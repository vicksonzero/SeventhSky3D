using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BReSpawner : MonoBehaviour {

    public Transform enemy;
    public int noofEnemy = 5;
    public float roomRadius = 300;
    public float maxEnemyDistance = 600;
    public BEnemyCounter enemyCounter;
    public BEnemyKillCounter enemyKillCounter;

    public float timeBetweenWaves = 1;

    public BPlayer player;



    private float maxEnemyDistanceSq;
    private List<BEnemy> enemies = new List<BEnemy>();

    // Use this for initialization
    void Start () {
        this.maxEnemyDistanceSq = this.maxEnemyDistance * this.maxEnemyDistance;
        this.enemyCounter.enemyCountUpdated += this.onEnemyCountUpdated;
        StartCoroutine(this.spawnNextWave(5));
        StartCoroutine(this.checkDistances(5));

    }

    // Update is called once per frame
    void Update () {
        this.transform.position = this.player.transform.position;

    }

    void onEnemyCountUpdated()
    {
        if(this.enemyCounter.count <= 0)
        {
            StartCoroutine(this.spawnNextWave(this.timeBetweenWaves));
        }
    }

    IEnumerator checkDistances(float interval = 1)
    {
        while (true) {
            yield return new WaitForSeconds(interval);
            var allTooFar = this.allTooFar();
            if (allTooFar)
            {
                //StopCoroutine("spawnNextWave");
                print("too far");
                this.removeAllEnemies();

                // this line will be called automatically due to enemy count changes
                //StartCoroutine(this.spawnNextWave(this.timeBetweenWaves));
            }
        }
    }

    private bool allTooFar()
    {
        BEnemy[] enemies = Object.FindObjectsOfType<BEnemy>();
        return enemies.ToList().All((enemy) => {
            var distanceSq = (enemy.transform.position - this.player.transform.position).sqrMagnitude;
            print(distanceSq);
            return (distanceSq > this.maxEnemyDistanceSq);
        });
    }

    private void removeAllEnemies()
    {
        BEnemy[] enemies = Object.FindObjectsOfType<BEnemy>();
        enemies.ToList().ForEach((enemy) => {
            enemy.remove();
        });
    }

    IEnumerator spawnNextWave(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        for (int i = this.noofEnemy; i-- > 0;)
        {
            Vector3 spawnPos = this.transform.position + Random.insideUnitSphere * roomRadius;
            Transform go = Instantiate(enemy, spawnPos, Quaternion.identity) as Transform;
            go.SetParent(this.transform);
            go.GetComponent<BEnemy>().enemyCounter = this.enemyCounter;
            go.GetComponent<BEnemy>().enemyKillCounter = this.enemyKillCounter;
            this.enemyCounter.login();
        }
    }
}
