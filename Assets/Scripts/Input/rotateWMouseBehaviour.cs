using UnityEngine;
using System.Collections;

public class rotateWMouseBehaviour : MonoBehaviour {

    public float rotSpeed = 116;

    private Quaternion originalRotation;

	// Use this for initialization
	void Start () {
        this.originalRotation = this.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (!Input.gyro.enabled && !Input.compass.enabled)
        {
            this.transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * this.rotSpeed, 0, Space.World);
            this.transform.Rotate(-1 * Input.GetAxis("Mouse Y") * Time.deltaTime * this.rotSpeed, 0, 0,Space.Self);
        }
	}
}
