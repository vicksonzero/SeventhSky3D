using UnityEngine;
using System.Collections;

public class BSpawner : MonoBehaviour {

    public Transform enemy;
    public int noofEnemy = 20;
    public float roomRadius = 12000;
    public BEnemyCounter enemyCounter;
    public BEnemyKillCounter enemyKillCounter;

	// Use this for initialization
	void Start () {
        for (int i = this.noofEnemy; i-->0; )
        {
            Vector3 spawnPos = this.transform.position + Random.insideUnitSphere * roomRadius;
            Transform go = Instantiate(enemy, spawnPos, Quaternion.identity) as Transform;
            go.SetParent(this.transform);
            go.GetComponent<BEnemy>().enemyCounter = this.enemyCounter;
            go.GetComponent<BEnemy>().enemyKillCounter = this.enemyKillCounter;
            this.enemyCounter.login();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
