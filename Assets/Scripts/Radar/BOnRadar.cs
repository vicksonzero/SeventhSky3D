using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOnRadar : MonoBehaviour {

    public Transform player;
    public Camera radarCamera;
    public RectTransform radarCanvas;
    public Transform radar3d;
    public RectTransform Icon;
    public RectTransform IconClose;
    public Transform IconOutsideView;
    public Transform IconRadar;
    public float iconDistance = 200;

    [Header("Private")]
    public RectTransform IconGO;
    public RectTransform IconCloseGO;
    public Transform IconOutsideViewGO;
    public Transform IconRadarGO;
    public float radarSize = 600;

	// Use this for initialization
	void Start () {
        GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);

        this.IconGO = Instantiate(this.Icon, Vector3.zero, Quaternion.identity) as RectTransform;
        this.IconGO.SetParent(this.radarCanvas);

        this.IconCloseGO = Instantiate(this.IconClose, Vector3.zero, Quaternion.identity) as RectTransform;
        this.IconCloseGO.SetParent(this.radarCanvas);

        this.IconOutsideViewGO = Instantiate(this.IconOutsideView, this.radar3d.position, Quaternion.identity) as Transform;
        this.IconOutsideViewGO.SetParent(this.radar3d);
        this.IconOutsideViewGO.localScale = new Vector3(1, 1, 1);

        this.IconRadarGO = Instantiate(this.IconRadar, this.radar3d.position, Quaternion.identity) as Transform;
        this.IconRadarGO.SetParent(this.radar3d);
        this.IconRadarGO.localScale = new Vector3(1, 1, 1);
        this.IconRadarGO.GetComponent<BRadarDot>().radarParent = this.radar3d;
	}
	
	// Update is called once per frame
	void Update () {

        float sqDistance = this.iconDistance * this.iconDistance;
        Vector3 relativePos = this.transform.position - this.player.transform.position;

        // disable all markers
        this.IconGO.GetComponent<Image>().enabled = false;
        this.IconCloseGO.GetComponent<Image>().enabled = false;
        this.IconOutsideViewGO.gameObject.SetActive(false);
        this.IconRadarGO.gameObject.SetActive(false);


        Vector3 screenPos = this.radarCamera.WorldToScreenPoint(this.transform.position);
        screenPos.z = 0;

        Vector3 relativeDir = this.player.transform.InverseTransformDirection(relativePos);

        if (relativeDir.z <= 0 || !this.inScreen(screenPos) )
        {
            // activate out-of-sight marker
            //this.drawOutOfSightMarker(relativePos);
        }
        else
        {
            if (relativePos.sqrMagnitude >= sqDistance)
            {
                // activate in-sight marker
                this.drawInSightFarMarker(screenPos);
            }
            else
            {
                // activate close range marker
                this.drawShootingRangeMarker(screenPos);
            }
        }
        if (relativePos.sqrMagnitude <= sqDistance)
        {
            this.drawRadarMarker(relativePos);
        }
        else
        {
            this.drawRadarMarker(relativePos.normalized * this.iconDistance);
        }
	}

    private void drawRadarMarker(Vector3 relativePos)
    {
        this.IconRadarGO.gameObject.SetActive(true);
        this.IconRadarGO.position = this.radar3d.transform.position + relativePos * this.radarSize / 2 / this.iconDistance;
        this.IconRadarGO.GetComponent<BRadarDot>().UpdateLineRenderer();
    }
    private void drawOutOfSightMarker(Vector3 relativePos)
    {
        this.IconOutsideViewGO.gameObject.SetActive(true);
        this.IconOutsideViewGO.rotation = Quaternion.LookRotation(relativePos);
    }
    private void drawInSightFarMarker(Vector3 screenPos)
    {
        this.IconGO.GetComponent<Image>().enabled = true;
        this.IconGO.transform.position = screenPos;
    }
    private void drawShootingRangeMarker(Vector3 screenPos)
    {
        this.IconCloseGO.GetComponent<Image>().enabled = true;
        this.IconCloseGO.transform.position = screenPos;
    }
    public void destroyMarker()
    {
        Destroy(this.IconGO.gameObject);
        Destroy(this.IconCloseGO.gameObject);
        Destroy(this.IconOutsideViewGO.gameObject);
        Destroy(this.IconRadarGO.gameObject);
    }
    public bool inScreen(Vector3 screenPos)
    {
        return (screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height);
    }
}
