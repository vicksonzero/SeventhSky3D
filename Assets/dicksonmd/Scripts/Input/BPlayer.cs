using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class BPlayer : BUnit
{

    [Header("Data")]

    public float topSpeed = 260;
    public float dashSpeed = 300;
    public float forwardforce = 450;
    public float brakedrag = 1.4f;
    //public float brakeBotSpeed = 20;
    public float turnSpeed = 20;
    private float normaldrag = 0.3f;

    [Header("Linking")]
    public Text speedLabel;
    public Button[] weaponButtons = new Button[3];
    public Animation model;
    public BPlayerHPBar hpBarListener;

    [Header("State")]
    public Quaternion targetRotation;

    public List<BShield> shields;

    public enum PacifixAnimState { Idle, IdleFlying, Forward, Dash, Dashing, Braking, HeadVulcan };
    private PacifixAnimState animState = PacifixAnimState.Idle;

    public enum ControlParty { GameMaster, Self, Skill, None };
    [Tooltip("Indicates who has control over this player")]
    public ControlParty isControlledBy = ControlParty.Self;

    [Tooltip("Attach weapon by putting components. This array will be filled in at runtime")]
    public BWeapons[] weapons = new BWeapons[4];

    public int currentWeaponIndex = 1;

    private Rigidbody rb;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        this.rb = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        string speedMeterText = "";
        int speedLevel = (int)(this.rb.velocity.magnitude / (this.topSpeed / 8.0f));
        for (int i = 0; i < speedLevel; i++)
        {
            speedMeterText += "=";
        }
        if (this.rb.velocity.magnitude > 0)
        {
            speedMeterText += "=";
        }
        this.speedLabel.text = string.Format("Speed: {0:F1}\n{1:S10}", this.rb.velocity.magnitude, speedMeterText);
        //this.speedLabel.text = string.Format("Speed: {0:F1}", this.rb.velocity.magnitude);

        this.updateRotation();
        this.updateAnimation();
    }

    private void updateRotation()
    {
        if (this.transform.rotation != this.targetRotation)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.targetRotation, Time.deltaTime * this.turnSpeed);
        }
    }

    public void accelerate()
    {
        //Debug.Log(this.rigidbody.velocity.magnitude);
        this.rb.AddRelativeForce(Vector3.forward * forwardforce);
        if (this.rb.velocity.magnitude > this.topSpeed)
        {
            this.rb.velocity *= this.topSpeed / this.rb.velocity.magnitude;
        }
        this.setAnimation(PacifixAnimState.Forward);

    }
    public void dash()
    {
        //Debug.Log(this.rigidbody.velocity.magnitude);
        this.rb.velocity = this.transform.forward * this.dashSpeed;
        this.setAnimation(PacifixAnimState.Dashing);

    }
    public void startDecelerate()
    {
        //Debug.Log(this.rigidbody.velocity.magnitude);
        //this.rigidbody.AddRelativeForce(this.rigidbody.velocity.normalized * -1 * this.brakeforce);
        this.normaldrag = this.rb.drag;
        this.rb.drag = this.brakedrag;

        this.setAnimation(PacifixAnimState.Braking);

    }
    public void stopDecelerate()
    {
        this.rb.drag = this.normaldrag;
        this.setAnimation(PacifixAnimState.Idle);
    }

    /// <summary>
    /// Trys to shoot the weapon
    /// </summary>
    /// <param name="index">index of the weapon, as registered in BPlayer#weapons</param>
    /// <returns>Whether the player did shoot the weapon</returns>
    public bool weaponTryShoot(int index = -1)
    {
        if (index == -1)
        {
            index = this.currentWeaponIndex;
        }
        return this.weapons[index].tryShoot();
    }

    public bool changeToWeapon(int index)
    {
        // TODO if can change, i.e.
        // is not changing weapon, weapon is not being used, etc
        // ask the weapon for it
        if (this.weapons[index] == null)
        {
            print("No weapon index " + index + ". did not change");
            return false;
        }
        var weapon = this.weapons[this.currentWeaponIndex];
        weapon.deselect();

        this.currentWeaponIndex = index;
        weapon = this.weapons[this.currentWeaponIndex];
        weapon.select();

        return true;
    }

    /// <summary>
    /// Trys to change the animation.
    /// Returns success or not depending on current animState and provided newState
    /// </summary>
    /// <param name="newState">The state you want to have in the next frame</param>
    /// <returns>animState changed or not</returns>
    public bool setAnimation(PacifixAnimState newState)
    {

        if (this.animState != newState)
        {
            if (this.animState == PacifixAnimState.Dash || this.animState == PacifixAnimState.Dashing)
            {
                if (newState == PacifixAnimState.Forward) return false;
            }
            this.animState = newState;
            return true;
        }
        return false;
    }

    private void updateAnimation()
    {
        string nextFrameName = "";
        //print(this.animState);

        // switch state depending on current state
        switch (this.animState)
        {
            case PacifixAnimState.Idle:
                nextFrameName = "Idle";
                break;

            case PacifixAnimState.IdleFlying:
                nextFrameName = "IdleFlying";
                if (this.rb.velocity.magnitude <= 30)
                {
                    this.setAnimation(PacifixAnimState.Idle);
                }
                break;

            case PacifixAnimState.Forward:
                nextFrameName = "Forward";
                this.setAnimation(PacifixAnimState.Idle);
                break;

            case PacifixAnimState.Dash:
                nextFrameName = "Dash";
                this.setAnimation(PacifixAnimState.Dashing);
                break;

            case PacifixAnimState.Dashing:
                nextFrameName = "Dashing";
                if (this.rb.velocity.magnitude <= this.topSpeed * 0.6)
                {
                    this.animState = PacifixAnimState.Forward;
                }
                break;

            case PacifixAnimState.Braking:
                nextFrameName = "Braking";
                if (this.rb.velocity.magnitude <= 10)
                {
                    this.setAnimation(PacifixAnimState.Idle);
                }
                break;

            case PacifixAnimState.HeadVulcan:
                nextFrameName = "HeadVulcan";
                this.setAnimation(PacifixAnimState.Idle);
                break;


        }
        this.model.CrossFade(nextFrameName, 0.3f, PlayMode.StopSameLayer);

        // invalidate animstate
        //this.animState = PacifixAnimState.Idle;
    }

    public bool weaponCanshoot(int index)
    {
        return this.weapons[index].canShoot();
    }

    public void weaponStartCooldown(int index)
    {
        this.weapons[index].startCooldown();
    }
    public float weaponGetCooldownPercent(int index)
    {
        return this.weapons[index].getCooldownPercent();
    }
    public void weaponRegister(int index, BWeapons weapon)
    {
        if (this.weapons[index] != null)
        {
            print("duplicate weapon index " + index + ". Overriding...");
        }
        this.weapons[index] = weapon;

        if (index >= 1)
        {
            var button = this.weaponButtons[index - 1];
            button.transform.Find("Text").GetComponent<Text>().text = weapon.weaponName;
            weapon.weaponButton = button;

            if (index == this.currentWeaponIndex)
            {
                weapon.holdWeaponTimer.startTimer();
            }
        }
    }

    public void registerShields(BShield shield)
    {
        this.shields.Add(shield);
    }

    public void enableShields()
    {
        this.shields.ForEach((BShield shield) => { shield.gameObject.SetActive(true); });
    }

    public void disableShields()
    {
        this.shields.ForEach((BShield shield) => { shield.gameObject.SetActive(false); });
    }


    protected override void onHPChanged()
    {
        this.hpBarListener.updateHPBar(this.hp, this.maxhp);
        base.onHPChanged();
    }

    public override void die()
    {

    }

    public void gaLogKeepWeaponTimes(float gameTime)
    {
        this.weapons.ToList().ForEach((weapon) => {
            if (weapon != null)
            {
                weapon.holdWeaponTimer.stopTimer();
                BAnalyticsGA.logKeepWeaponTime(weapon.weaponName, (weapon.holdWeaponTimer.elapsedTime / gameTime)*100);
            }
        });
    }

    //public void gaLogSwitcWeaponCounts()
    //{
    //    this.weapons.ToList().ForEach((weapon) => {
    //        if (weapon != null)
    //        {
    //            weapon.holdWeaponTimer.stopTimer();
    //            BAnalyticsGA(weapon.weaponName, weapon.holdWeaponTimer.elapsedTime);
    //        }
    //    });
    //}
}
