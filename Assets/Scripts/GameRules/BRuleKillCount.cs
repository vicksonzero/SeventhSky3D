using UnityEngine;
using System.Collections;

public class BRuleKillCount : BRules
{

    public int targetKills = 20;
    public BGameMaster gm;
    public BEnemyKillCounter counter;

	// Use this for initialization
	void Start () {
        this.counter.addListener(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public override void checkStat()
    {
        if(this.counter.count >= this.targetKills){
            this.gm.gameOver(true);
        }
    }
}
