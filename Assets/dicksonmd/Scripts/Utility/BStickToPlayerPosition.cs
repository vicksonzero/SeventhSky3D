using UnityEngine;
using System.Collections;

public class BStickToPlayerPosition : MonoBehaviour {
    public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.identity;
	}
}
