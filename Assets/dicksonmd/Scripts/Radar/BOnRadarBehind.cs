using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOnRadarBehind : MonoBehaviour
{
    public RectTransform IconBehind;

    [Header("Private")]
    private RectTransform IconBehindGO;

    private BOnRadar onRadar;

    // Use this for initialization
    void Start()
    {
        //GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);
        //this.GetComponent<BUnit>().hpBarListener = this;

        this.onRadar = this.GetComponent<BOnRadar>();
        this.onRadar.beforeDestroyMarker += this.destroyMarker;

        this.IconBehindGO = Instantiate(this.IconBehind, Vector3.zero, Quaternion.identity) as RectTransform;
        this.IconBehindGO.name = "IconOutOfSightGO";
        this.IconBehindGO.SetParent(this.onRadar.radarCanvas);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (this.onRadar.relativeDir.z <= 0 || !this.onRadar.inScreen)
        {
            // activate out-of-sight marker
            this.drawOutOfSightMarker(this.onRadar.relativePos);
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
            relativePosFromPlayer *= (float)(0.5 * Screen.height * 0.90);
            this.IconBehindGO.position = relativePosFromPlayer + this.onRadar.radarCanvas.position;

    }
    public void destroyMarker()
    {

        if (this.IconBehindGO)
        {
            Destroy(this.IconBehindGO.gameObject);
        }
    }
}
