using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOnRadar : MonoBehaviour
{

    [Header("Fill in")]
    public RectTransform Icon;
    public float iconDistance = 200;

    public delegate void BeforeDestroyMarker();
    public event BeforeDestroyMarker beforeDestroyMarker;

    [Header("Private")]

    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public Camera radarCamera;
    [HideInInspector]
    public RectTransform radarCanvas;
    [HideInInspector]
    public Transform radar3d;

    public BitArray turnOn = new BitArray(7);
    public enum turnOnIndex { IconGO, IconCloseGO, IconOutsideViewGO, IconRadarGO, hpBarGO, nameTagGO, IconOutOfSightGO };

    [HideInInspector]
    public float sqDistance = 0;

    [HideInInspector]
    public Vector3 relativePos = Vector3.zero;

    [HideInInspector]
    public Vector3 screenPos = Vector3.zero;

    [HideInInspector]
    public Vector3 relativeDir = Vector3.zero;

    [HideInInspector]
    public float radar3DSize = 200;

    [HideInInspector]
    public bool inScreen = false;

    // Use this for initialization
    void Start()
    {
        GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);
        //this.GetComponent<BEnemy>().hpBarListener = this;

    }

    // Update is called once per frame
    void Update()
    {
        this.sqDistance = this.iconDistance * this.iconDistance;
        this.relativePos = this.transform.position - this.player.transform.position;

        this.screenPos = this.radarCamera.WorldToScreenPoint(this.transform.position);
        this.screenPos.z = 0;

        this.relativeDir = this.player.transform.InverseTransformDirection(this.relativePos);

        this.updateInScreen();
    }

    public void updateInScreen()
    {
        //return (screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height);
        this.inScreen = Vector3.Distance(this.screenPos, new Vector3(Screen.width / 2, Screen.height / 2, 0)) <= 0.4 * Screen.height;
    }

    public void destroyMarker()
    {
        this.beforeDestroyMarker();
    }

    void OnDisable()
    {
        if (this.beforeDestroyMarker != null) this.beforeDestroyMarker();
    }
}
