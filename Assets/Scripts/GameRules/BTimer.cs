using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BTimer : MonoBehaviour {

    public string timerID = "Timer";
    public float startTime = 0;

    public Text[] counterLabels;

    public void startTimer()
    {
        this.startTime = Time.time;
    }
    public void stopTimer()
    {

    }
    public float readTimer()
    {
        float elapsed = Time.time - this.startTime;
        return elapsed;
    }
    public string readTimerFormatted()
    {
        float elapsed = Time.time - this.startTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsed);
        string timeText = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        return timeText;
    }
    public void updateLabels()
    {
        foreach (Text element in this.counterLabels)
        {
            element.text = this.readTimerFormatted();
        }
    }

}
