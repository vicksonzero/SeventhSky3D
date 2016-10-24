using UnityEngine;
using System.Collections;

public class BSAMRotateStablizers : MonoBehaviour {

    public float rotSpeed = 3;

    public Transform stablizers;
	
	// Update is called once per frame
	void Update () {
        this.stablizers.Rotate(Vector3.up, this.rotSpeed * Time.deltaTime);

    }
}
