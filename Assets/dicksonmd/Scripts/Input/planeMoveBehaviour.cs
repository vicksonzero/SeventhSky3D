using UnityEngine;
using System.Collections;

public class planeMoveBehaviour : MonoBehaviour {

    public float topSpeed = 200;
	// Use this for initialization
	void Start () {
        Debug.Log("planeMoveBehaviour Start()");
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.forward * topSpeed * Time.deltaTime);//this.transform.rotation.eulerAngles*speed*Time.deltaTime);
	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(300f,300f,100f,50f),"Reset plane"))
        {
            this.transform.position = Vector3.zero;
        }
        GUI.Label(new Rect(200,0,400,100),"Plane: " + transform.position);
    }
}
