using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BStatCounters : MonoBehaviour {

    public delegate void CounterUpdatedDelegate();

    //[Header("BStatCounters")]

    public event CounterUpdatedDelegate updated;

    
    
    public void trigger()
    {
        if (this.updated != null)
        {
            this.updated();
        }
    }

}
