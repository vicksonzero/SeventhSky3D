using UnityEngine;
using System.Collections;

public class LapTimer {

    public float elapsedTime { get; private set; }
	
    public bool isRunning { get; private set; }

    private float startTime;

    public bool startTimer()
    {
        if (this.isRunning) return false;

        this.startTime = Time.time;
        this.isRunning = true;
        return true;
    }

    public bool stopTimer()
    {
        if (this.isRunning)
        {
            this.elapsedTime += Time.time - this.startTime;
            this.isRunning = false;
            return true;
        }

        return false;
    }

    public LapTimer reset()
    {
        this.elapsedTime = 0;
        this.isRunning = false;
        this.startTime = Time.time;

        return this;
    }

    public float probeElapsedTime()
    {
        if (this.isRunning)  return this.elapsedTime + (Time.time - this.startTime);

        return this.elapsedTime;
    }

}
