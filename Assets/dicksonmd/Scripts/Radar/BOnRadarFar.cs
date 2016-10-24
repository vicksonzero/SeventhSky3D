using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOnRadarFar : MonoBehaviour
{

    [Header("Private")]

    [HideInInspector]
    public Transform IconFarGO;

    [HideInInspector]
    public float radar3DSize = 200;

    private BOnRadar onRadar;

    // Use this for initialization
    void Start()
    {
        //GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);

        this.onRadar = this.GetComponent<BOnRadar>();
        this.onRadar.beforeDestroyMarker += this.destroyMarker;

        this.IconFarGO = Instantiate(this.onRadar.Icon, Vector3.zero, Quaternion.identity) as RectTransform;
        this.IconFarGO.name = "IconGO";
        this.IconFarGO.SetParent(this.onRadar.radarCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!(this.onRadar.relativeDir.z <= 0 || !this.onRadar.inScreen) &&
            this.onRadar.relativePos.sqrMagnitude > this.onRadar.sqDistance)
        {
            // activate close range marker
            this.drawInSightFarMarker(this.onRadar.screenPos);

            this.IconFarGO.gameObject.SetActive(true);
        }
        else
        {
            this.IconFarGO.gameObject.SetActive(false);
        }


    }

    private void drawInSightFarMarker(Vector3 screenPos)
    {
        //this.IconGO.GetComponent<Image>().enabled = true;
        if (this.IconFarGO) this.IconFarGO.transform.position = screenPos;
    }

    public void destroyMarker()
    {
        if (this.IconFarGO)
        {
            Destroy(this.IconFarGO.gameObject);
        }
    }
}
