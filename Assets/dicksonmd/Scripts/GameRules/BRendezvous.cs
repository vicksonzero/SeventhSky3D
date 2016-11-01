using UnityEngine;
using System.Collections;

public class BRendezvous : MonoBehaviour {

    public delegate void OnEnterDelegate(BUnit unit);
    public delegate void OnLeaveDelegate(BUnit unit);

    public event OnEnterDelegate onEnter;
    public event OnLeaveDelegate onLeave;

    // Use this for initialization
    void Start () {
	    
	}
	void OnTriggerEnter(Collider other)
    {
        var bunit = other.GetComponentInParent<BUnit>();
        if(bunit != null)
        {
            if(onEnter != null)
            {
                onEnter(bunit);
            }
        }
    }
    void OnTriggerLeave(Collider other)
    {
        var bunit = other.GetComponent<BUnit>();
        if (bunit != null)
        {
            if (onLeave != null)
            {
                onLeave(bunit);
            }
        }
    }
}
