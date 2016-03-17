using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BPlayer : MonoBehaviour {

    public float hpMax = 100;
    public float hp = 100;
    public bool isInvincible = false;

    public float topSpeed = 600;
	public float forwardforce = 300;
    public float dashInterval = 0.5f;
    public float brakedrag = 1.3f;
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

    public float dashCountdown = 0;
    public float dashCount = 0;
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
        if ( Input.GetButton("Acceleration") )
        {
            this.accelerate();
            
        }
        if (Input.GetButtonDown("Acceleration"))
        {
            if (this.dashCountdown > 0 && this.dashCount == 1)
            {
                this.dash();
            }
            else if (this.dashCountdown <= 0)
            {
                this.dashCountdown = this.dashInterval;
                this.dashCount = 1;
            }
        }
        Debug.Log(this.dashCountdown);
        if (this.dashCountdown > 0)
        {
            this.dashCountdown -= 1 * Time.deltaTime;
        }
        else
        {
            this.dashCount = 0;
        }
        if (Input.GetButtonDown("Brake"))
        {
            this.startDecelerate();
        }
        if (Input.GetButtonUp("Brake"))
        {
            this.stopDecelerate();
        }
#if UNITY_EDITOR
        if (Input.GetAxis("Fire1")>0)
        {
            this.weapons[1].tryShoot();
        }
#endif

        this.updateAnimation();
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
