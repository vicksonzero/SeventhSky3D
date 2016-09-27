using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class BPlayer : MonoBehaviour {

    [Header("Data")]
    public float maxhp = 100;

    public float topSpeed = 600;
    public float dashSpeed = 600;
    public float forwardforce = 300;
    public float brakedrag = 1.3f;
    //public float brakeBotSpeed = 20;
    public float turnSpeed = 10;
    private float normaldrag = 0.3f;

    [Header("Linking")]
    public Text speedLabel;
    public Button[] weaponButtons = new Button[3];
    public Animation model;
    public BPlayerHPBar hpBarListener;

    [Header("State")]
    public float hp = 100;
    public bool isInvincible = false;

    public Quaternion targetRotation;

    public List<BShield> shields;

    public enum PacifixAnimState { Idle, IdleFlying, Forward, Dash, Dashing, Braking, HeadVulcan };
    private PacifixAnimState animState = PacifixAnimState.Idle;

    public enum ControlParty { GameMaster, Self, Skill, None };
    [Tooltip("Indicates who has control over this player")]
    public ControlParty isControlledBy = ControlParty.Self;

    [Tooltip("Attach weapon by putting components. This array will be filled in at runtime")]
    public BWeapons[] weapons = new BWeapons[4];


    public delegate void DamageTakenDelegate();

    //[Header("BStatCounters")]

    public event DamageTakenDelegate damageTaken;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        this.rb = this.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        string speedMeter = "";
        int speedLevel = (int)(this.rb.velocity.magnitude/(400.0/8.0));
        for(int i=0; i < speedLevel; i++)
        {
            speedMeter += "=";
        }
        this.speedLabel.text = string.Format("Speed: {0:F1}\n{1:S10}", this.rb.velocity.magnitude, speedMeter);
        //this.speedLabel.text = string.Format("Speed: {0:F1}", this.rb.velocity.magnitude);

        this.updateRotation();
        this.updateAnimation();
	}

    private void updateRotation()
    {
        if(this.transform.rotation != this.targetRotation)
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
        this.setAnimation(PacifixAnimState.Dash);

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
    public bool weaponTryShoot(int index)
    {
        return this.weapons[index].tryShoot();
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
            if(this.animState == PacifixAnimState.Dash || this.animState == PacifixAnimState.Dashing)
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
                if (this.rb.velocity.magnitude <= 350)
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
        this.weapons[index] = weapon;
        if (index >= 1)
        {
            this.weaponButtons[index - 1].transform.Find("Text").GetComponent<Text>().text = this.weapons[index].weaponName;
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

    public void takeDamage(float amount)
    {
        this.hp -= amount;

        if (this.hp <= 0)
        {
            this.die();
        }
        this.onHPChanged();
    }

    private void onHPChanged()
    {
        this.damageTaken();
        this.hpBarListener.updateHPBar(this.hp, this.maxhp);
    }

    public float getHPPercent()
    {
        return this.hp / this.maxhp;
    }
    public void die()
    {
        
    }
}
