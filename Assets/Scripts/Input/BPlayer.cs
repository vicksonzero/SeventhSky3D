using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BPlayer : MonoBehaviour {

    public float hpMax = 100;
    public float hp = 100;
    public bool isInvincible = false;

    public float topSpeed = 600;
	public float forwardforce = 300;
    public float brakedrag = 1.7f;
    //public float brakeBotSpeed = 20;
    private float normaldrag = 0.3f;
    public Transform model;

    public enum PacifixAnimState { Idle, Forward };
    private PacifixAnimState animState = PacifixAnimState.Idle;

    public enum ControlParty{GameMaster, Self, Skill, None};
    [Tooltip("Indicates who has control over this player")]
    public ControlParty isControlledBy = ControlParty.Self;

    [Tooltip("Will be filled in at runtime")]
    public BWeapons[] weapons = new BWeapons[4];

    public Text speedLabel;
    public Button[] weaponButtons = new Button[3];
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        this.speedLabel.text = "Speed: " + this.rigidbody.velocity.magnitude.ToString();
        if ( Input.GetKey("space") )
        {
            this.accelerate();
        }
        this.updateAnimation();
	}

    public void accelerate()
    {
        //Debug.Log(this.rigidbody.velocity.magnitude);
        this.rigidbody.AddRelativeForce(Vector3.forward * forwardforce);
        if (this.rigidbody.velocity.magnitude > this.topSpeed)
        {
            this.rigidbody.velocity *= this.topSpeed / this.rigidbody.velocity.magnitude;
        }
        this.setAnimation(PacifixAnimState.Forward);
            
    }
    public void dash()
    {
        //Debug.Log(this.rigidbody.velocity.magnitude);
        this.rigidbody.velocity = Vector3.forward * forwardforce;
        this.setAnimation(PacifixAnimState.Forward);

    }
    public void startDecelerate()
    {
        //Debug.Log(this.rigidbody.velocity.magnitude);
        //this.rigidbody.AddRelativeForce(this.rigidbody.velocity.normalized * -1 * this.brakeforce);
        this.normaldrag = this.rigidbody.drag;
        this.rigidbody.drag = this.brakedrag;
        this.setAnimation(PacifixAnimState.Idle);

    }
    public void stopDecelerate()
    {
        this.rigidbody.drag = this.normaldrag;
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
        this.model.animation.CrossFade(nextFrameName, 0.3f, PlayMode.StopSameLayer);

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
