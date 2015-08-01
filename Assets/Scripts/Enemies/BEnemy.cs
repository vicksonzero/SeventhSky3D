using UnityEngine;
using System.Collections;

public class BEnemy : MonoBehaviour {


    public float hp = 100;
    public float maxhp = 100;
    public Transform firework;
    public BEnemyCounter enemyCounter;
    public BEnemyKillCounter enemyKillCounter;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void takeDamage(float amount)
    {
        this.hp -= amount;

        if (this.hp <= 0)
        {
            this.die();
        }

    }

    public float getHPPercent()
    {
        return this.hp / this.maxhp;
    }
    public void die()
    {
        this.GetComponent<BOnRadar>().destroyMarker();
        Instantiate(this.firework, this.transform.position, Quaternion.identity);
        this.enemyCounter.logout();
        this.enemyKillCounter.add();

        Destroy(this.gameObject);
    }
}
