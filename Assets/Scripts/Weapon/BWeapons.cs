using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BPlayer))]
public abstract class BWeapons :MonoBehaviour {

    [Header("TODO: separate vaalue object from implements")]

    [Header("General info")]
    public int weaponIndex = 0;
    public string weaponName;
    public Sprite icon;

    public float cooldownTime;
    private float cooldownElapsed;

    public float damage = 0;

    public BPlayer player;

    public void Start()
    {
        print("weapon start");
        this.GetComponent<BPlayer>().weaponRegister(this.weaponIndex, this);
        this.init();
    }

    public void init()
    {
        print("weapon init");
        this.resetCooldown();
    }

    void Update()
    {
        if (this.cooldownElapsed < this.cooldownTime)
        {
            this.cooldownElapsed += Time.deltaTime;
        }
        else
        {
            this.cooldownElapsed = this.cooldownTime;
        }
    }

    public bool canShoot()
    {
        return this.cooldownElapsed >= this.cooldownTime;
    }

    public float getCooldownPercent()
    {
        return Mathf.Clamp(this.cooldownElapsed / this.cooldownTime, 0, 1);
    }

    public bool startCooldown()
    {
        this.cooldownElapsed = 0;
        return true;
    }
    public void resetCooldown()
    {
        this.cooldownElapsed = this.cooldownTime;
    }

    public abstract void doShoot();

    public bool tryShoot()
    {
        if (this.canShoot())
        {
            this.doShoot();
            this.startCooldown();
            return true;
        }
        else
        {
            return false;
        }
    }
}
