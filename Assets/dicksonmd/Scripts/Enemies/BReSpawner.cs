﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BReSpawner : MonoBehaviour {

    public int noofEnemy = 6;
    public BUnit[] enemyChoices;
    public float[] enemyWeights;
    public float roomRadius = 300;
    public float maxEnemyDistance = 400;

    [Tooltip("In seconds")]
    public float timeBetweenWaves = 1;

    [Header("Linking")]
    public BEnemyCounter enemyCounter;
    public BEnemyKillCounter enemyKillCounter;


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
        return enemies.ToList().All((enemy) => {
            var distanceSq = (enemy.transform.position - this.player.transform.position).sqrMagnitude;
            //print(distanceSq);
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
        var originalPosition = this.player.transform.position;

        var createdCount = 0;

        for (int i = this.noofEnemy; i-- > 0;)
        {
            Vector3 spawnPos = originalPosition + Random.insideUnitSphere * roomRadius;
            Transform enemy = this.pickAnEnemy();
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
        }
    }

    Transform pickAnEnemy()
    {
        var weights = new Dictionary<BUnit, float>();
        for(int i=0; i < this.enemyChoices.Length && i < this.enemyWeights.Length; i++)
        {
            weights.Add(this.enemyChoices[i], this.enemyWeights[i]); // 90% spawn chance;

        }

        BUnit selected = WeightedRandomizer.From(weights).TakeOne(); // Strongly-typed object returned. No casting necessary.
        print(selected);
        return selected.transform;
    }
}
