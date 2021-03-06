﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(BPlayer))]
public abstract class BWeapons :MonoBehaviour {

    [Header("TODO: separate vaalue object from implements")]

    [Header("General info")]
    public int weaponIndex = 0;
    public Button weaponButton;
    public string weaponName;
    public Sprite icon;

    public float cooldownTime;
    private float cooldownElapsed;

    public float damage = 0;

    protected BPlayer player;

    public LapTimer holdWeaponTimer = new LapTimer();

    public int selectedCounts = 0;

    public delegate void SelectedDelegate();
    public event SelectedDelegate selectedEvent;

    public delegate void DeelectedDelegate();
    public event DeelectedDelegate deselectedEvent;

    public virtual void Start()
    {
        print("weapon start");
        this.GetComponent<BPlayer>().weaponRegister(this.weaponIndex, this);
        this.init();
    }

    public virtual void init()
    {
        print("weapon init");
        this.resetCooldown();
    }

    protected virtual void Update()
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

    public virtual void select()
    {
        this.selectedCounts++;
        this.holdWeaponTimer.startTimer();
        if (this.selectedEvent != null) this.selectedEvent();
    }

    public virtual void deselect()
    {
        this.holdWeaponTimer.stopTimer();
        if (this.deselectedEvent != null) this.deselectedEvent();
    }
}
