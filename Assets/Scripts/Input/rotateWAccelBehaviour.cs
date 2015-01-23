using UnityEngine;
using System.Collections;

public class rotateWAccelBehaviour : MonoBehaviour {

    public bool liveDebug = false;
    private class GUIVar { public string azmuth = "", roll = "", pitch="", accel = "",accel_orig="";};
    GUIVar gui_var = new GUIVar();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        //1
        ////Vector3 lookDir = Input.compass.rawVector;
        //float azmuth = Input.compass.magneticHeading;
        //gui_azmuth = azmuth.ToString();
        //float roll = Input.acceleration.x;
        //gui_roll = roll.ToString();

        //this.transform.rotation = Quaternion.Euler(0,azmuth,roll);
        //this.transform.Rotate(Vector3.forward*Input.acceleration.x*10);
        //2
        //Vector3 true_accel = Input.acceleration + new Vector3(0, +1.0f, 0);
        //this.gui_var.accel = true_accel.ToString();
        //this.transform.Rotate(Vector3.up * true_accel.y);

        //3
        float azmuth = Input.compass.magneticHeading;
        gui_var.azmuth = azmuth.ToString();
        float pitch = Input.acceleration.z*-90;
        gui_var.pitch = pitch.ToString();
        float roll = Input.acceleration.x * -90;
        gui_var.roll = roll.ToString();


        //this.transform.rotation = Quaternion.Euler(pitch, azmuth, roll);
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(pitch, azmuth, roll), 0.2f);
	
	}
    void OnGUI()
    {
        if (liveDebug)
        {
            this.gui_var.accel_orig = Input.acceleration.ToString();
            string str =
                "azmuth:" + this.gui_var.azmuth +
                "\npitch:" + this.gui_var.pitch +
                "\nroll:" + this.gui_var.roll +
                "\naccel(0)" + this.gui_var.accel_orig +
                "\naccel(1)" + this.gui_var.accel;
            GUI.Label(new Rect(10, 10, 200, 300), str);
        }
    }
}
