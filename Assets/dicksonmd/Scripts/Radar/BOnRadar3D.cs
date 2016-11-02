using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(BOnRadar))]
public class BOnRadar3D : MonoBehaviour
{
    public Transform IconRadar;

    public bool useLine = true;

    [Header("TODO: behind is not yet implemented")]
    public bool useDifferentBehindIcon = true;
    public Transform IconRadarBehind;

    [Header("Private")]

    [HideInInspector]
    public Transform IconRadarGO;

    [HideInInspector]
    public Transform IconRadarBehindGO;

    private BOnRadar onRadar;
    private BUnit unit;

    // Use this for initialization
    void Start()
    {
        //GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);

        this.onRadar = this.GetComponent<BOnRadar>();

        if (this.onRadar == null)
        {
            print("onRadar cannot be null.");
            this.enabled = false;
        }
        this.onRadar.beforeDestroyMarker += this.destroyMarker;

        this.IconRadarGO = Instantiate(this.IconRadar, this.onRadar.radar3d.position, Quaternion.identity) as Transform;
        this.IconRadarGO.SetParent(this.onRadar.radar3d);
        this.IconRadarGO.localScale = new Vector3(1, 1, 1);
        this.IconRadarGO.localRotation = Quaternion.identity;
        this.IconRadarGO.GetComponent<BRadarDot>().radarParent = this.onRadar.radar3d;
        if (useDifferentBehindIcon)
        {
            this.IconRadarBehindGO = Instantiate(this.IconRadarBehind, this.onRadar.radar3d.position, Quaternion.identity) as Transform;
            this.IconRadarBehindGO.SetParent(this.onRadar.radar3d);
            this.IconRadarBehindGO.localScale = new Vector3(1, 1, 1);
            this.IconRadarBehindGO.localRotation = Quaternion.identity;
            this.IconRadarBehindGO.GetComponent<BRadarDot>().radarParent = this.onRadar.radar3d;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Transform activeIcon = this.IconRadarGO;
        if (this.useDifferentBehindIcon)
        {
            if (this.onRadar != null)
            {
                if (this.onRadar.relativeDir.z <= 0)
                {
                    this.IconRadarGO.gameObject.SetActive(false);
                    this.IconRadarBehindGO.gameObject.SetActive(true);
                    activeIcon = this.IconRadarBehindGO;
                }
                else
                {
                    this.IconRadarGO.gameObject.SetActive(true);
                    this.IconRadarBehindGO.gameObject.SetActive(false);
                    activeIcon = this.IconRadarGO;
                }
            }
        }



        //var magnitude = this.onRadar.relativePos.magnitude;
        var relativePos = this.onRadar.relativePos.normalized * this.onRadar.iconDistance;
        //if (magnitude <= this.onRadar.iconDistance)
        //{
        //    this.drawRadarMarker(activeIcon, relativePos * Mathf.Sin(Mathf.PI / 2 * magnitude / this.onRadar.iconDistance));
        //    activeIcon.gameObject.SetActive(true);
        //}
        //else
        //{
        this.drawRadarMarker(activeIcon, relativePos);
        activeIcon.gameObject.SetActive(true);
        //}


    }

    private void drawRadarMarker(Transform icon, Vector3 relativePos)
    {
        //this.IconRadarGO.gameObject.SetActive(true);
        if (icon)
        {
            icon.position = this.onRadar.radar3d.transform.position + relativePos * this.onRadar.radar3DSize / 2 / this.onRadar.iconDistance;
            //icon.GetComponent<BRadarDot>().UpdateLineRenderer();

        }
    }

    public void destroyMarker()
    {
        if (this.IconRadarGO)
        {
            Destroy(this.IconRadarGO.gameObject);
        }
        if (this.IconRadarBehindGO)
        {
            Destroy(this.IconRadarBehindGO.gameObject);
        }
    }
}
