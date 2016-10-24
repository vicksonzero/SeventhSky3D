using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOnRadar1 : MonoBehaviour
{

    public Transform player;
    public Camera radarCamera;
    public RectTransform radarCanvas;
    public Transform radar3d;
    public RectTransform Icon;
    public RectTransform IconClose;
    public Transform IconOutsideView;
    public Transform IconRadar;
    [Tooltip("icon when it is behind you")]
    public RectTransform IconOutOfSight;
    public float iconDistance = 200;
    public RectTransform hpBar;
    public Vector2 hpBarOffset;
    public RectTransform nameTag;
    public Vector2 nameTagOffset;

    [Header("Private")]
    public RectTransform IconGO;
    public RectTransform IconCloseGO;
    public RectTransform IconOutOfSightGO;
    public Transform IconOutsideViewGO;
    public Transform IconRadarGO;
    public RectTransform hpBarGO;
    public RectTransform nameTagGO;

    public BitArray turnOn = new BitArray(7);
    public enum turnOnIndex { IconGO, IconCloseGO, IconOutsideViewGO, IconRadarGO, hpBarGO, nameTagGO, IconOutOfSightGO };

    [HideInInspector]
    public float radar3DSize = 200;


    // Use this for initialization
    void Start()
    {
        //GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);
        //this.GetComponent<BEnemy>().hpBarListener = this;

        this.IconGO = Instantiate(this.Icon, Vector3.zero, Quaternion.identity) as RectTransform;
        this.IconGO.name = "IconGO";
        this.IconGO.SetParent(this.radarCanvas);

        this.IconCloseGO = Instantiate(this.IconClose, Vector3.zero, Quaternion.identity) as RectTransform;
        this.IconCloseGO.SetParent(this.radarCanvas);

        this.IconOutsideViewGO = Instantiate(this.IconOutsideView, this.radar3d.position, Quaternion.identity) as Transform;
        this.IconOutsideViewGO.SetParent(this.radar3d);
        this.IconOutsideViewGO.localScale = new Vector3(1, 1, 1);

        this.IconOutOfSightGO = Instantiate(this.IconOutOfSight, this.radar3d.position, Quaternion.identity) as RectTransform;
        this.IconOutOfSightGO.name = "IconOutOfSightGO";
        this.IconOutOfSightGO.SetParent(this.radarCanvas);

        this.IconRadarGO = Instantiate(this.IconRadar, this.radar3d.position, Quaternion.identity) as Transform;
        this.IconRadarGO.SetParent(this.radar3d);
        this.IconRadarGO.localScale = new Vector3(1, 1, 1);
        this.IconRadarGO.localRotation = Quaternion.identity;
        this.IconRadarGO.GetComponent<BRadarDot>().radarParent = this.radar3d;

        this.hpBarGO = Instantiate(this.hpBar, Vector2.Scale(this.hpBarOffset, new Vector2(1, 1)), Quaternion.identity) as RectTransform;
        this.hpBarGO.SetParent(this.IconCloseGO, false);
        this.hpBarGO.transform.position = this.hpBarOffset;

        this.nameTagGO = Instantiate(this.nameTag, this.nameTagOffset, Quaternion.identity) as RectTransform;
        this.nameTagGO.SetParent(this.IconCloseGO, false);
        this.nameTagGO.transform.localPosition = this.nameTagOffset;
    }

    // Update is called once per frame
    void Update()
    {
        this.turnOn.SetAll(false);

        float sqDistance = this.iconDistance * this.iconDistance;
        Vector3 relativePos = this.transform.position - this.player.transform.position;

        Vector3 screenPos = this.radarCamera.WorldToScreenPoint(this.transform.position);
        screenPos.z = 0;

        Vector3 relativeDir = this.player.transform.InverseTransformDirection(relativePos);

        if (relativeDir.z <= 0 || !this.inScreen(screenPos))
        {
            // activate out-of-sight marker
            this.drawOutOfSightMarker(relativePos);
            this.turnOn[(int)BOnRadar.turnOnIndex.IconOutOfSightGO] = true;
        }
        else
        {
            if (relativePos.sqrMagnitude >= sqDistance)
            {
                // activate in-sight marker
                this.drawInSightFarMarker(screenPos);
                this.turnOn[(int)BOnRadar.turnOnIndex.IconGO] = true;
            }
            else
            {
                // activate close range marker
                this.drawShootingRangeMarker(screenPos);
                this.turnOn[(int)BOnRadar.turnOnIndex.IconCloseGO] = true;

                this.drawHPBar(screenPos);
                this.turnOn[(int)BOnRadar.turnOnIndex.hpBarGO] = true;
            }
        }
        if (relativePos.sqrMagnitude <= sqDistance)
        {
            this.drawRadarMarker(relativePos);
            this.turnOn[(int)BOnRadar.turnOnIndex.IconRadarGO] = true;
        }
        else
        {
            this.drawRadarMarker(relativePos.normalized * this.iconDistance);
            this.turnOn[(int)BOnRadar.turnOnIndex.IconRadarGO] = true;
        }

        // disable all markers
        if (this.IconGO) this.IconGO.gameObject.SetActive(this.turnOn[(int)BOnRadar.turnOnIndex.IconGO]);
        if (this.IconCloseGO) this.IconCloseGO.gameObject.SetActive(this.turnOn[(int)BOnRadar.turnOnIndex.IconCloseGO]);
        if (this.IconOutsideViewGO) this.IconOutsideViewGO.gameObject.SetActive(this.turnOn[(int)BOnRadar.turnOnIndex.IconOutsideViewGO]);
        if (this.hpBarGO) this.hpBarGO.gameObject.SetActive(this.turnOn[(int)BOnRadar.turnOnIndex.hpBarGO]);
        if (this.IconOutOfSightGO) this.IconOutOfSightGO.gameObject.SetActive(this.turnOn[(int)BOnRadar.turnOnIndex.IconOutOfSightGO]);
    }

    private void drawHPBar(Vector3 screenPos)
    {
        Vector2 scaleVector = new Vector2(1, 1);
        //this.hpBarGO.sizeDelta.x

        //if (screenPos.x + this.hpBarGO.sizeDelta.x / 2 + this.hpBarOffset.x > this.radarCanvas.sizeDelta.x) scaleVector.x = -1;
        //if (screenPos.y + this.hpBarGO.sizeDelta.y / 2 + this.hpBarOffset.y > this.radarCanvas.sizeDelta.y) scaleVector.y = -1;
        //this.hpBarGO.gameObject.SetActive(true);
        //Vector2 newPos = new Vector2(screenPos.x, screenPos.y) + Vector2.Scale(this.hpBarOffset, scaleVector);
        //this.hpBarGO.transform.position = newPos;
    }

    public void updateHPBar(float hp, float hp_max)
    {
        if (this.hpBarGO)
        {
            this.hpBarGO.GetComponent<Slider>().maxValue = hp_max;
            this.hpBarGO.GetComponent<Slider>().value = hp;
        }
    }
    private void drawNameTag(Vector3 relativePos)
    {
        //this.IconRadarGO.gameObject.SetActive(true);
        if (this.IconRadarGO)
        {
            this.IconRadarGO.position = this.radar3d.transform.position + relativePos * this.radar3DSize / 2 / this.iconDistance;
            this.IconRadarGO.GetComponent<BRadarDot>().UpdateLineRenderer();

        }
    }

    private void drawRadarMarker(Vector3 relativePos)
    {
        //this.IconRadarGO.gameObject.SetActive(true);
        if (this.IconRadarGO)
        {
            this.IconRadarGO.position = this.radar3d.transform.position + relativePos * this.radar3DSize / 2 / this.iconDistance;
            this.IconRadarGO.GetComponent<BRadarDot>().UpdateLineRenderer();

        }
    }
    private void drawOutOfSightMarker(Vector3 relativePos)
    {
        //this.IconOutsideViewGO.gameObject.SetActive(true);
        //this.IconOutsideViewGO.rotation = Quaternion.LookRotation(relativePos);
        if (this.IconOutOfSightGO)
        {
            Vector3 relativePosFromPlayer = this.player.InverseTransformVector(relativePos);
            relativePosFromPlayer.z = 0;
            relativePosFromPlayer.Normalize();
            relativePosFromPlayer *= (float)(0.5 * Screen.height * 0.95);
            this.IconOutOfSightGO.position = relativePosFromPlayer + radarCanvas.position;

        }
    }
    private void drawInSightFarMarker(Vector3 screenPos)
    {
        //this.IconGO.GetComponent<Image>().enabled = true;
        if (this.IconGO) this.IconGO.transform.position = screenPos;
    }
    private void drawShootingRangeMarker(Vector3 screenPos)
    {
        //this.IconCloseGO.GetComponent<Image>().enabled = true;
        if (this.IconCloseGO) this.IconCloseGO.transform.position = screenPos;
    }
    public void destroyMarker()
    {
        Destroy(this.IconGO.gameObject);
        Destroy(this.IconCloseGO.gameObject);
        Destroy(this.IconOutsideViewGO.gameObject);
        Destroy(this.IconOutOfSightGO.gameObject);
        Destroy(this.IconRadarGO.gameObject);
    }
    public bool inScreen(Vector3 screenPos)
    {
        //return (screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height);
        return (screenPos - new Vector3(Screen.width / 2, Screen.height / 2, 0)).magnitude <= 0.5 * Screen.height;
    }
}
