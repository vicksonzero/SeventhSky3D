using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class BEnemyKillCounter : BStatCounters
{
    public enum enemyName { };
    [Header("BEnemyKillCounter")]
    public int count;

    public Text[] counterLabels;


	// Use this for initialization
	void Start () {
        this.count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void add()
    {
        this.count++;
        this.trigger();
    }

    public void subtract()
    {
        this.count--;
        this.trigger();
    }

    public void updateLabels(){
        foreach (Text element in this.counterLabels)
        {
            element.text = this.count.ToString();
        }
    }
}
