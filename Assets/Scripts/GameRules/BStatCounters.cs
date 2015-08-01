using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BStatCounters : MonoBehaviour {

	[Header("BStatCounters")]
    public List<BRules> listeners;


    public void addListener(BRules subscriber)
    {
        this.listeners.Add(subscriber);
    }
    public void trigger()
    {
        foreach (BRules element in this.listeners)
        {
            element.checkStat();
        }
    }

}
