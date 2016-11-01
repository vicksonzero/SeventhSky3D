using UnityEngine;
using System.Collections;

public class BRadarDot : MonoBehaviour {

    public float thickness = 10;
    public Color color = new Color(255, 255, 255);

    public Transform radarParent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void UpdateLineRenderer () {
        LineRenderer lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.SetColors(this.color, this.color);
        lineRenderer.material.color = this.color;
        lineRenderer.SetWidth(this.thickness, this.thickness);
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, new Vector3(0,0,0));
        Vector3 basePos = this.transform.localPosition;
        basePos.x = 0;
        basePos.z = 0;
        basePos.y = -basePos.y;
        lineRenderer.SetPosition(1, basePos);
	    //radarParent
	}
}
