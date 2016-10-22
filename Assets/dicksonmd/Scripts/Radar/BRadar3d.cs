using UnityEngine;
using System.Collections;

public class BRadar3d : MonoBehaviour {

    public Transform player;
    public Camera radarCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addMember(BOnRadar go)
    {
        go.player = this.player;
        go.radarCamera = this.radarCamera;
    }
}
