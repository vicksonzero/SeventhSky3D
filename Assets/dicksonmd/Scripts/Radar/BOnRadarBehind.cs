using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOnRadarBehind : MonoBehaviour
{
    public RectTransform IconBehind;

    [Header("Private")]
    public RectTransform IconBehindGO;

    private BOnRadar onRadar;

    // Use this for initialization
    void Start()
    {
        //GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);
        //this.GetComponent<BUnit>().hpBarListener = this;
        

        this.IconBehindGO = Instantiate(this.IconBehind, Vector3.zero, Quaternion.identity) as RectTransform;
        this.IconBehindGO.name = "IconOutOfSightGO";
        this.IconBehindGO.SetParent(this.onRadar.radarCanvas);
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 relativePos = this.transform.position - this.onRadar.player.transform.position;

        Vector3 screenPos = this.onRadar.radarCamera.WorldToScreenPoint(this.transform.position);
        screenPos.z = 0;

        Vector3 relativeDir = this.onRadar.player.transform.InverseTransformDirection(relativePos);

        if (relativeDir.z <= 0 || !this.onRadar.inScreen(screenPos))
        {
            // activate out-of-sight marker
            this.drawOutOfSightMarker(relativePos);
            this.IconBehindGO.gameObject.SetActive(true);
        }
        else
        {
            this.IconBehindGO.gameObject.SetActive(false);
        }
        
    }
    
    private void drawOutOfSightMarker(Vector3 relativePos)
    {
        //this.IconOutsideViewGO.gameObject.SetActive(true);
        //this.IconOutsideViewGO.rotation = Quaternion.LookRotation(relativePos);
            Vector3 relativePosFromPlayer = this.onRadar.player.InverseTransformVector(relativePos);
            relativePosFromPlayer.z = 0;
            relativePosFromPlayer.Normalize();
            relativePosFromPlayer *= (float)(0.5 * Screen.height * 0.95);
            this.IconBehindGO.position = relativePosFromPlayer + this.onRadar.radarCanvas.position;

    }
    public void destroyMarker()
    {
        Destroy(this.IconBehindGO.gameObject);
    }
}
