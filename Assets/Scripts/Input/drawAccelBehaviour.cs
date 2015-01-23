using UnityEngine;
using System.Collections;

public class drawAccelBehaviour : MonoBehaviour {

    public LineRenderer lr;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(lr.
        lr.SetVertexCount(2);
        lr.SetPosition(0, Vector3.zero);
        //lr.SetPosition(1, Input.acceleration.normalized*5);
        lr.SetPosition(1, Quaternion.AngleAxis(90, Vector3.right) * Input.acceleration.normalized * 5);
        
	}
    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    private Quaternion GetRotFix()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
            return Quaternion.identity;
        if (Screen.orientation == ScreenOrientation.LandscapeLeft
        || Screen.orientation == ScreenOrientation.Landscape)
            return Quaternion.Euler(0, 0, -90);
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
            return Quaternion.Euler(0, 0, 90);
        if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            return Quaternion.Euler(0, 0, 180);
        return Quaternion.identity;
    }

}
