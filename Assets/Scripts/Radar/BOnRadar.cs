using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOnRadar : MonoBehaviour {

    public Transform player;
    public Camera radarCamera;
    public RectTransform radarCanvas;
    public RectTransform Icon;

    public RectTransform IconGO;
    public float iconDistance = 200;

	// Use this for initialization
	void Start () {
        GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);
        this.IconGO = Instantiate(this.Icon, Vector3.zero, Quaternion.identity) as RectTransform;
        this.IconGO.SetParent(this.radarCanvas);
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 relativePos = this.transform.position - this.player.transform.position;
        float sqDistance = this.iconDistance * this.iconDistance;

        this.IconGO.GetComponent<Image>().enabled = false;
        if (relativePos.sqrMagnitude >= sqDistance)
        {
            Vector3 relativeDir = this.player.transform.InverseTransformDirection(relativePos);
            if (relativeDir.z > 0)
            {
                Vector3 screenPos = this.radarCamera.WorldToScreenPoint(this.transform.position);
                screenPos.z = 0;
                this.IconGO.GetComponent<Image>().enabled = true;
                this.IconGO.transform.position = screenPos;
            }
        }
	}
    public void destroyMarker()
    {
        Destroy(this.IconGO.gameObject);
    }
}
