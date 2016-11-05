using UnityEngine;
using System.Collections;
using System;

public class BScreenshotButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Screenshot"))
        {
            this.takeScreenshot();
        }
    }

    public void takeScreenshot()
    {
        string dateString = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
        print(Application.persistentDataPath);
        Application.CaptureScreenshot("screenshots/Screenshot " + dateString + ".png");
    }
}
