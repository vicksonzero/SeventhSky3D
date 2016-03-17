using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BGameMaster : MonoBehaviour {

    public BPlayer player;
    public BMissionAccomplished missionAccomplishedManager;
    public BTimer gameTimeTimer;

    public string sceneName;

    public lockCursorBehaviour lockCursorBehaviour;

    // Use this for initialization
    void Start () {
        BTimer[] timers = this.GetComponents<BTimer>();
        foreach(BTimer timer in timers){
            if (timer.timerID == "gameTime")
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
        print("Game over: " + (isWin?"WIN":"LOSE"));
        if (isWin)
        {
            // protect player
            this.player.isInvincible = true;

            // disable player
            this.player.isControlledBy = BPlayer.ControlParty.GameMaster;

            // start end game animation
            this.missionAccomplishedManager.sequenceEndCallbacks += onMissionAccomplishedSequanceFinished;
            this.missionAccomplishedManager.startSequence();
            this.gameTimeTimer.updateLabels();

            //Application.Quit();
        }
    }

    public void onMissionAccomplishedSequanceFinished()
    {

        //Application.LoadLevel(sceneName);
        if (lockCursorBehaviour!=null) lockCursorBehaviour.ReleaseCursor();
        SceneManager.LoadScene(sceneName);
    }
}
