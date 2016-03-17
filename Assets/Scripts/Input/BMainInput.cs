using UnityEngine;
using System.Collections;
using System;

public class BMainInput : MonoBehaviour {

    [Header("Fill in")]
    public BPlayer player;
    public float dashInterval = 0.5f;

    [Header("private, inspected")]
    public float dashCountdown = 0;
    public float dashCount = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = this.player.transform.position;
        if (Input.GetButton("Acceleration"))
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
        if (Input.GetAxis("Fire1") > 0)
        {
            this.weaponTryShoot(1);
        }
#endif
    }

    private void updatePlayerRotation()
    {
        this.player.targetRotation = this.transform.rotation;
    }


    public void accelerate()
    {
        this.player.accelerate();
        this.updatePlayerRotation();
    }
    public void dash()
    {
        this.player.dash();
    }
    public void startDecelerate()
    {
        this.player.startDecelerate();
    }
    public void stopDecelerate()
    {
        this.player.stopDecelerate();
    }
    public bool weaponTryShoot(int index)
    {
        this.updatePlayerRotation();
        return this.player.weaponTryShoot(index);
    }
}
