using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BRuleKillCount : BRules
{
    public int targetKills = 20;
    public BGameMaster gm;
    public BEnemyKillCounter counter;
    public Text counterLabel;

    // Use this for initialization
    void Start () {
        this.counter.updated+=this.checkStat;
        this.counterLabel.text = "Targets left: " + (this.targetKills - this.counter.count).ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public override void checkStat()
    {
        this.counterLabel.text = "Targets left: " + (this.targetKills - this.counter.count).ToString();
        if (this.isActive && this.counter.count >= this.targetKills)
        {
            this.isActive = false;
            this.gm.gameOver(true);
        }
    }
}
