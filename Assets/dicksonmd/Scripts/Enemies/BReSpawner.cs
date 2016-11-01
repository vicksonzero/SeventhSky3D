using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BReSpawner : MonoBehaviour
{
    public bool startAutomatically = false;

    [Tooltip("In seconds")]
    public float timeBetweenWaves = 1;

    public int noofEnemy = 6;
    public BUnit[] enemyChoices;
    public float[] enemyWeights;
    public float roomRadius = 300;
    public float maxEnemyDistance = 400;

    [Header("Linking")]
    public BEnemyCounter enemyCounter;
    public BEnemyKillCounter enemyKillCounter;


    public BPlayer player;



    private float maxEnemyDistanceSq;
    private List<BEnemy> enemies = new List<BEnemy>();
    private Coroutine spawningTimer;

    private LapTimer gaReadManualTime = new LapTimer();

    // Use this for initialization
    void Start()
    {
        this.maxEnemyDistanceSq = this.maxEnemyDistance * this.maxEnemyDistance;
        this.enemyCounter.enemyCountUpdated += this.onEnemyCountUpdated;
        StartCoroutine(this.checkDistances(5));


        if (startAutomatically)
        {
            this.startSpawning();
        }
        else
        {
            this.gaReadManualTime.startTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onEnemyCountUpdated()
    {
        if (this.enemyCounter.count <= 0)
        {
            StartCoroutine(this.spawnNextWave(this.timeBetweenWaves));
        }
    }

    IEnumerator checkDistances(float interval = 1)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            var allTooFar = this.allTooFar();
            if (allTooFar)
            {
                //StopCoroutine("spawnNextWave");
                //print("too far");
                this.removeAllEnemies();

                // this line will be called automatically due to enemy count changes
                //StartCoroutine(this.spawnNextWave(this.timeBetweenWaves));
            }
        }
    }

    private bool allTooFar()
    {
        BEnemy[] enemies = Object.FindObjectsOfType<BEnemy>();
        return enemies.ToList().All((enemy) =>
        {
            var distanceSq = (enemy.transform.position - this.player.transform.position).sqrMagnitude;
            //print(distanceSq);
            return (distanceSq > this.maxEnemyDistanceSq);
        });
    }

    private void removeAllEnemies()
    {
        BEnemy[] enemies = Object.FindObjectsOfType<BEnemy>();
        enemies.ToList().ForEach((enemy) =>
        {
            enemy.remove();
        });
        BEnemyMissile[] missiles = Object.FindObjectsOfType<BEnemyMissile>();
        missiles.ToList().ForEach((missile) =>
        {
            missile.GetComponent<BUnit>().remove();
        });
    }

    IEnumerator spawnNextWave(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        var originalPosition = this.player.transform.position;

        var createdCount = 0;

        var counts = new Dictionary<BUnit, int>();
        for (int i = 0; i < this.enemyChoices.Length && i < this.enemyWeights.Length; i++)
        {
            counts.Add(this.enemyChoices[i], 0);
        }

        for (int i = this.noofEnemy; i-- > 0;)
        {
            Vector3 spawnPos = originalPosition + Random.insideUnitSphere * roomRadius;
            Transform enemy = this.pickAnEnemy();
            counts[enemy.GetComponent<BUnit>()]++;
            Transform go = Instantiate(enemy, spawnPos, Quaternion.identity) as Transform;
            go.SetParent(this.transform);
            go.GetComponent<BEnemy>().enemyCounter = this.enemyCounter;
            go.GetComponent<BEnemy>().enemyKillCounter = this.enemyKillCounter;
            this.enemyCounter.login();
            createdCount++;
        }
        if (createdCount > 0)
        {
            GameObject.FindObjectOfType<BUISound>().playNewWaveAlert();

            var countString = counts.Aggregate(
                "",
                (result, pair) =>
                {
                    if (pair.Value > 0)
                    {
                        return result + (result == "" ? "" : ", ") + pair.Value + " " + pair.Key.unitName;
                    }
                    else {
                        return result;
                    }
                });
            BUIMessage.log("Incoming enemies: \n" + countString);
        }
    }

    Transform pickAnEnemy()
    {
        var weights = new Dictionary<BUnit, float>();
        for (int i = 0; i < this.enemyChoices.Length && i < this.enemyWeights.Length; i++)
        {
            weights.Add(this.enemyChoices[i], this.enemyWeights[i]); // 90% spawn chance;

        }

        BUnit selected = WeightedRandomizer.From(weights).TakeOne(); // Strongly-typed object returned. No casting necessary.
        return selected.transform;
    }

    public void startSpawning()
    {
        //var hadStopped = false;
        if (this.spawningTimer != null)
        {
            StopCoroutine(this.spawningTimer);
            //hadStopped = true;
        }
        this.spawningTimer = StartCoroutine(this.spawnNextWave(5));

        if (this.gaReadManualTime.isRunning)
        {
            this.gaReadManualTime.stopTimer();

            BAnalyticsGA.logReadManualTime(this.gaReadManualTime.elapsedTime);
            BAnalyticsGA.logGameStart();
        }
        else
        {
            print("gaReadManualTime is not running =.=");
        }

        //return hadStopped;
    }

    public bool stopSpawning()
    {
        if (this.spawningTimer != null)
        {
            StopCoroutine(this.spawningTimer);
            return true;
        }
        return false;
    }

}
