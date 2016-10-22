using UnityEngine;
using System.Collections;

public class BRuleCannotDie : BRules
{
    public BGameMaster gm;
    public BPlayer player;

    // Use this for initialization
    void Start()
    {
        this.player.damageTaken += this.checkStat;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void checkStat()
    {
        if (this.isActive && this.player.getHPPercent() <= 0)
        {
            this.isActive = false;
            this.gm.gameOver(false);
        }
    }
}
