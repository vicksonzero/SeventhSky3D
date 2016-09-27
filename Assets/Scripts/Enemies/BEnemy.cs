using UnityEngine;
using System.Collections;

public class BEnemy : MonoBehaviour {


    public float hp = 100;
    public float maxhp = 100;
    public Transform firework;
    public BEnemyCounter enemyCounter;
    public BEnemyKillCounter enemyKillCounter;
    public BOnRadar hpBarListener;

    private BGameMaster game;

	// Use this for initialization
	void Start () {
        this.game = GameObject.FindObjectOfType<BGameMaster>();
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
        this.onHPChanged();
    }

    private void onHPChanged()
    {
        this.hpBarListener.updateHPBar(this.hp, this.maxhp);
    }

    public float getHPPercent()
    {
        return this.hp / this.maxhp;
    }

    public void die()
    {
        Instantiate(this.firework, this.transform.position, Quaternion.identity);
        this.enemyKillCounter.add();
        this.remove();
    }

    public void remove()
    {
        print("remove()");

        this.GetComponent<BOnRadar>().destroyMarker();
        this.enemyCounter.logout();
        Destroy(this.gameObject);
    }
}
