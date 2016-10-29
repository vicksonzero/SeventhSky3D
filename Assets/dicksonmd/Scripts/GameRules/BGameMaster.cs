using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class BGameMaster : MonoBehaviour
{

    public BPlayer player;
    public BMissionAccomplished missionAccomplishedManager;
    public BMissionFailed missionFailedManager;
    public BTimer gameTimeTimer;

    public string sceneName;

    public BTeamManager teamManager;

    public lockCursorBehaviour lockCursorBehaviour;

    public GoogleAnalyticsV4 googleAnalytics;

    // Use this for initialization
    void Start()
    {
        BTimer[] timers = this.GetComponents<BTimer>();
        foreach (BTimer timer in timers)
        {
            if (timer.timerID == "gameTime")
            {
                this.gameTimeTimer = timer;
                timer.startTimer();
            }
        }


        print("TeamManager = \n" + this.teamManager.ToString());
    }
    
    

    // Update is called once per frame
    void Update()
    {

    }

    public void gameOver(bool isWin)
    {
        print("Game over: " + (isWin ? "WIN" : "LOSE"));

        // common

        // protect player
        this.player.isInvincible = true;

        // disable player
        this.player.isControlledBy = BPlayer.ControlParty.GameMaster;

        // GA logging
        this.player.gaLogKeepWeaponTimes(this.gameTimeTimer.readTimer());

        BAnalyticsGA.logGameEnd(isWin);

        if (isWin)
        {
            // start end game animation
            this.missionAccomplishedManager.sequenceEndCallbacks += onMissionAccomplishedSequanceFinished;
            this.missionAccomplishedManager.startSequence();
            this.gameTimeTimer.updateLabels();

            //Application.Quit();
        }
        else
        {
            // start end game animation
            this.missionFailedManager.sequenceEndCallbacks += onMissionAccomplishedSequanceFinished;
            this.missionFailedManager.startSequence();
            this.gameTimeTimer.updateLabels();

            //Application.Quit();
        }
    }

    public void onMissionAccomplishedSequanceFinished()
    {

        //Application.LoadLevel(sceneName);
        if (lockCursorBehaviour != null) lockCursorBehaviour.ReleaseCursor();
        SceneManager.LoadScene(sceneName);
    }
}
