using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BPlayer : MonoBehaviour {

    [Header("Data")]
    public float hpMax = 100;

    public float topSpeed = 600;
	public float forwardforce = 300;
    public float brakedrag = 1.3f;
    //public float brakeBotSpeed = 20;
    public float turnSpeed = 10;
    private float normaldrag = 0.3f;

    [Header("Linking")]
    public Text speedLabel;
    public Button[] weaponButtons = new Button[3];
    public Transform model;

    [Header("State")]
    public float hp = 100;
    public bool isInvincible = false;

    public Quaternion targetRotation;

    public enum PacifixAnimState { Idle, Forward };
    private PacifixAnimState animState = PacifixAnimState.Idle;

    public enum ControlParty { GameMaster, Self, Skill, None };
    [Tooltip("Indicates who has control over this player")]
    public ControlParty isControlledBy = ControlParty.Self;

    [Tooltip("Attach weapon by putting components. This array will be filled in at runtime")]
    public BWeapons[] weapons = new BWeapons[4];

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        string speedMeter = "";
        int speedLevel = (int)(this.GetComponent<Rigidbody>().velocity.magnitude/(400.0/8.0));
        for(int i=0; i < speedLevel; i++)
        {
            speedMeter += "=";
        }
        this.speedLabel.text = string.Format("Speed: {0:F1}\n{1:S10}", this.GetComponent<Rigidbody>().velocity.magnitude, speedMeter);
        //this.speedLabel.text = string.Format("Speed: {0:F1}", this.GetComponent<Rigidbody>().velocity.magnitude);

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
        this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * forwardforce);
        if (this.GetComponent<Rigidbody>().velocity.magnitude > this.topSpeed)
        {
            this.GetComponent<Rigidbody>().velocity *= this.topSpeed / this.GetComponent<Rigidbody>().velocity.magnitude;
        }
        this.setAnimation(PacifixAnimState.Forward);
            
    }
    public void dash()
    {
        //Debug.Log(this.rigidbody.velocity.magnitude);
        this.GetComponent<Rigidbody>().velocity = this.transform.forward * this.topSpeed;
        this.setAnimation(PacifixAnimState.Forward);

    }
    public void startDecelerate()
    {
        //Debug.Log(this.rigidbody.velocity.magnitude);
        //this.rigidbody.AddRelativeForce(this.rigidbody.velocity.normalized * -1 * this.brakeforce);
        this.normaldrag = this.GetComponent<Rigidbody>().drag;
        this.GetComponent<Rigidbody>().drag = this.brakedrag;
        this.setAnimation(PacifixAnimState.Idle);

    }
    public void stopDecelerate()
    {
        this.GetComponent<Rigidbody>().drag = this.normaldrag;
    }

    public bool weaponTryShoot(int index)
    {
        return this.weapons[index].tryShoot();
    }


    void setAnimation(PacifixAnimState newState)
    {

        if (this.animState != newState)
        {
            this.animState = newState;
        }

    }
    private void updateAnimation()
    {
        string nextFrameName = "";
        switch (this.animState)
        {
            case PacifixAnimState.Idle:
                nextFrameName = "Idle";

                break;
            case PacifixAnimState.Forward:
                nextFrameName = "Forward";

                break;
        }
        this.model.GetComponent<Animation>().CrossFade(nextFrameName, 0.3f, PlayMode.StopSameLayer);

        // invalidate animstate
        this.animState = PacifixAnimState.Idle;
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
}
