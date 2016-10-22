using UnityEngine;
using System.Collections;

public abstract class BRules : MonoBehaviour {

    public bool isActive = false;
	// Use this for initialization
    public abstract void checkStat();
}
