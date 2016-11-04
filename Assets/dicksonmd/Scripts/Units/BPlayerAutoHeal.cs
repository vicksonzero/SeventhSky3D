using UnityEngine;
using System.Collections;

public class BPlayerAutoHeal : MonoBehaviour {

    public float healInterval = 4;
    public float healAmount = 1;

    private BPlayer player;

    private float gaHealAmount = 0;

	// Use this for initialization
	void Start () {
        this.player = this.GetComponent<BPlayer>();
        StartCoroutine(this.healTick());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator healTick()
    {
        while (true)
        {
            if(PlayerPrefs.GetInt("useAutoHeal", 0) == 1)
            {
                var lostHp = this.player.maxhp - this.player.hp;
                BAnalyticsGA.healedAmount += (lostHp > this.healAmount ? this.healAmount : lostHp);
                this.player.takeDamage(-1 * this.healAmount);
            }
            yield return new WaitForSeconds(this.healInterval);
        }
    }

}
