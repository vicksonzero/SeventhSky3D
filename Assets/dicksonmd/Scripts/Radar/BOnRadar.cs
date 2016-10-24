using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOnRadar : MonoBehaviour
{

    [Header("Fill in")]
    public Transform player;
    public Camera radarCamera;
    public RectTransform radarCanvas;
    public Transform radar3d;
    public RectTransform Icon;

    [Header("Private")]

    public BitArray turnOn = new BitArray(7);
    public enum turnOnIndex { IconGO, IconCloseGO, IconOutsideViewGO, IconRadarGO, hpBarGO, nameTagGO, IconOutOfSightGO };

    [HideInInspector]
    public float radar3DSize = 200;

    // Use this for initialization
    void Start()
    {
        GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);
        this.GetComponent<BEnemy>().hpBarListener = this;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool inScreen(Vector3 screenPos)
    {
        //return (screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height);
        return (screenPos - new Vector3(Screen.width / 2, Screen.height / 2, 0)).magnitude <= 0.5 * Screen.height;
    }

    public void destroyMarker()
    {

    }
}
