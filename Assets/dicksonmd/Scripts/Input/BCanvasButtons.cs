using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BCanvasButtons : MonoBehaviour {

    private bool forwardIsDown = false;
    private bool brakeIsDown = false;
    private float forwardDblClickTimer = 0;

    private bool[] shootIsDown = new bool[4];

    private bool shieldIsDown = false;


    public BMainInput mainInput;

    public float dashCountdown = 0;
    public int dashCount = 0;

    public float dashInterval = 0.5f;

    public bool useScreenshotButton = false;
    public Button screenshotButton; 

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (this.forwardIsDown)
        {
            this.HForwardStep();
        }

        if (this.brakeIsDown)
        {
            this.HBrakeStep();
        }

        for (int i = this.shootIsDown.Length; i-- > 0; )
        {
            if (this.shootIsDown[i])
            {
                this.HShootStep(i);
            }
        }

        if (this.shieldIsDown)
        {
            this.HShieldsStep();
        }

        if (this.dashCountdown > 0)
        {
            this.dashCountdown -= 1 * Time.deltaTime;
        }
        else
        {
            this.dashCount = 0;
        }
    }
    public void HForwardDown()
    {
        this.forwardIsDown = true;

        if (this.dashCountdown > 0 && this.dashCount == 1)
        {
            this.mainInput.dash();
        }
        else if (this.dashCountdown <= 0)
        {
            this.dashCountdown = this.dashInterval;
            this.dashCount = 1;
        }
    }
    public void HForwardStep()
    {
        mainInput.accelerate();
    }
    public void HForwardUp()
    {
        this.forwardIsDown = false;
    }

    public void HBrakeDown()
    {
        this.brakeIsDown = true;
        this.mainInput.startDecelerate();
    }
    public void HBrakeStep()
    {
        // nothing
    }
    public void HBrakeUp()
    {
        this.brakeIsDown = false;
        this.mainInput.stopDecelerate();
    }

    public void HShootDown(int index)
    {
        this.shootIsDown[index] = true;

    }

    public void HShootStep(int index)
    {
        mainInput.weaponTryShoot();
    }

    public void HShootUp(int index)
    {
        this.shootIsDown[index] = false;
    }

    public void HShieldsDown()
    {
        this.shieldIsDown = true;

    }
    public void HShieldsStep()
    {

        mainInput.enableShields();
    }
    public void HShieldsUp()
    {
        mainInput.disableShields();
        this.shieldIsDown = false;
    }

    public void HSwitchWeaponDown(int index)
    {
        mainInput.changeToWeapon(index);
    }

    public void HSwitchWeaponStep(int index)
    {

    }

    public void HSwitchWeaponUp(int index)
    {

    }
}
