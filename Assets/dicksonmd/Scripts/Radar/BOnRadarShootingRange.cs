using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BOnRadarShootingRange : MonoBehaviour
{

    public bool useIconClose = true;
    public RectTransform IconClose;

    public bool useHpBar = true;
    public RectTransform hpBar;
    public Vector2 hpBarOffset;

    public bool useNameTag = true;
    public string nameTagName = "name";
    public RectTransform nameTag;
    public Vector2 nameTagOffset;

    [Header("Private")]

    [HideInInspector]
    public RectTransform IconCloseGO;

    [HideInInspector]
    public RectTransform hpBarGO;

    [HideInInspector]
    public RectTransform nameTagGO;

    [HideInInspector]
    public float radar3DSize = 200;

    private BOnRadar onRadar;
    private BUnit unit;

    // Use this for initialization
    void Start()
    {
        //GameObject.Find("Radar").GetComponent<BRadar>().addMember(this);

        this.onRadar = this.GetComponent<BOnRadar>();
        this.onRadar.beforeDestroyMarker += this.destroyMarker;

        if (this.useIconClose)
        {
            this.IconCloseGO = Instantiate(this.IconClose, Vector3.zero, Quaternion.identity) as RectTransform;
            //print(this.name + " " + this.onRadar.radarCanvas);
            this.IconCloseGO.SetParent(this.onRadar.radarCanvas);
        }

        if (this.useHpBar)
        {
            this.hpBarGO = Instantiate(this.hpBar, Vector2.Scale(this.hpBarOffset, new Vector2(1, 1)), Quaternion.identity) as RectTransform;
            this.hpBarGO.SetParent(this.IconCloseGO, false);
            this.hpBarGO.transform.position = this.hpBarOffset;

            this.unit = this.GetComponent<BUnit>();
            this.unit.hpChanged += this.onHpUpdated;
        }

        if (this.useNameTag)
        {
            this.nameTagGO = Instantiate(this.nameTag, this.nameTagOffset, Quaternion.identity) as RectTransform;
            this.nameTagGO.SetParent(this.IconCloseGO, false);
            this.nameTagGO.transform.localPosition = this.nameTagOffset;
            this.nameTagGO.GetComponent<Text>().text = this.nameTagName;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (!(this.onRadar.relativeDir.z <= 0 || !this.onRadar.inScreen) &&
            this.onRadar.relativePos.sqrMagnitude <= this.onRadar.sqDistance)
        {
            // activate close range marker
            this.drawShootingRangeMarker(this.onRadar.screenPos);

            if (this.useIconClose) this.IconCloseGO.gameObject.SetActive(true);
            if (this.useHpBar) this.hpBarGO.gameObject.SetActive(true);
        }
        else
        {
            if (this.useIconClose) this.IconCloseGO.gameObject.SetActive(false);
            if (this.useHpBar) this.hpBarGO.gameObject.SetActive(false);
        }


    }

    public void onHpUpdated()
    {
        if (this.hpBarGO)
        {
            this.hpBarGO.GetComponent<Slider>().maxValue = this.unit.maxhp;
            this.hpBarGO.GetComponent<Slider>().value = this.unit.hp;
        }
    }
    private void drawShootingRangeMarker(Vector3 screenPos)
    {
        //this.IconCloseGO.GetComponent<Image>().enabled = true;
        if (this.IconCloseGO) this.IconCloseGO.transform.position = screenPos;
    }

    public void destroyMarker()
    {
        if (this.IconCloseGO)
        {
            Destroy(this.IconCloseGO.gameObject);
        }
    }
}
