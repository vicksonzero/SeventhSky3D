using UnityEngine;
using System.Collections;

public class BCanvasButtons : MonoBehaviour {

    private bool forwardIsDown = false;
    private bool brakeIsDown = false;
    private float forwardDblClickTimer = 0;

    private bool[] shootIsDown = new bool[4];


    public BPlayer player;

    public float dashCountdown = 0;
    public int dashCount = 0;

    public float dashInterval = 0.5f;

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
            this.player.dash();
        }
        else if (this.dashCountdown <= 0)
        {
            this.dashCountdown = this.dashInterval;
            this.dashCount = 1;
        }
    }
    public void HForwardStep()
    {
        player.accelerate();
    }
    public void HForwardUp()
    {
        this.forwardIsDown = false;
    }

    public void HBrakeDown()
    {
        this.brakeIsDown = true;
        this.player.startDecelerate();
    }
    public void HBrakeStep()
    {
        // nothing
    }
    public void HBrakeUp()
    {
        this.brakeIsDown = false;
        this.player.stopDecelerate();
    }

    public void HShootDown(int index)
    {
        this.shootIsDown[index] = true;

    }
    public void HShootStep(int index)
    {
        player.weaponTryShoot(1);
    }
    public void HShootUp(int index)
    {
        this.shootIsDown[index] = false;
    }
}
