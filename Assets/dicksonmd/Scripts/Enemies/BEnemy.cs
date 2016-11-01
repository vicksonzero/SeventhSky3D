using UnityEngine;
using System.Collections;

public class BEnemy : BUnit {


    public BEnemyCounter enemyCounter;
    public BEnemyKillCounter enemyKillCounter;

    private BGameMaster game;


    // Use this for initialization
    public override void Start () {
        base.Start();
        this.game = GameObject.FindObjectOfType<BGameMaster>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    protected override void onHPChanged()
    {
        base.onHPChanged();
    }

    public override void die()
    {
        if (this.enemyKillCounter != null) this.enemyKillCounter.add();
        BAnalyticsGA.logKill(this.unitName);
        base.die();
    }

    public override void remove()
    {
        //print("remove()");

        if(this.enemyCounter !=null) this.enemyCounter.logout();
        this.GetComponent<BOnRadar>().destroyMarker();

        base.remove();
    }
}
