using UnityEngine;
using System.Collections;

public class BGameMaster : MonoBehaviour {

    public BPlayer player;
    public BMissionAccomplished missionAccomplishedManager;
    public BTimer gameTimeTimer;

	// Use this for initialization
	void Start () {
        BTimer[] timers = this.GetComponents<BTimer>();
        foreach(BTimer timer in timers){
            if (timer.name == "gameTime")
            {
                this.gameTimeTimer = timer;
                timer.startTimer();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void gameOver(bool isWin)
    {
        print("Game over: " + isWin);
        if (isWin)
        {
            // protect player
            this.player.isInvincible = true;

            // disable player
            this.player.isControlledBy = BPlayer.ControlParty.GameMaster;

            // start end game animation
            this.missionAccomplishedManager.startSequence();
            this.gameTimeTimer.updateLabels();

            //Application.Quit();
        }
    }
}
