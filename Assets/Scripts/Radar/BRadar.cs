using UnityEngine;
using System.Collections;

public class BRadar : MonoBehaviour {

    public Transform player;
    public Camera radarCamera;
    public RectTransform radarCanvas;

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
        go.radarCanvas = this.radarCanvas;
    }
}
