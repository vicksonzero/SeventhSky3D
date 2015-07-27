using UnityEngine;
using System.Collections;

public class BCanvasButtons : MonoBehaviour {

    private bool forwardIsDown = false;
    private bool brakeIsDown = false;
    private float forwardDblClickTimer = 0;

    private bool[] shootIsDown = new bool[4];


    public BPlayer player;

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
	}
    public void HForwardDown()
    {
        this.forwardIsDown = true;

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

    }
    public void HBrakeStep()
    {
        player.accelerate();
    }
    public void HBrakeUp()
    {
        this.brakeIsDown = false;
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
