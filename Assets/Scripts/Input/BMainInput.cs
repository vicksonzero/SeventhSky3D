using UnityEngine;
using System.Collections;
using System;

public class BMainInput : MonoBehaviour {

    [Header("Fill in")]
    public BPlayer player;
    public float dashInterval = 0.3f;
    public BCameraDragAim bCameraDragAim;

    [Header("private, just for inspection")]
    public float dashCountdown = 0;
    public float dashCount = 0;


    // Use this for initialization
    void Start () {

        if (this.bCameraDragAim == null) this.bCameraDragAim = this.GetComponent<BCameraDragAim>();

    }
	
	// Update is called once per frame
	void Update () {
        // follow player
        this.bCameraDragAim.transform.position = this.player.transform.position;


        // input
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

        //if (Input.GetAxis("Fire1") > 0)
        //{
        //    this.weaponTryShoot(1);
        //}

        this.tuneCamera(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

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

    public void enableShields()
    {
        this.updatePlayerRotation();
        this.player.enableShields();
    }

    public void disableShields()
    {
        this.player.disableShields();
    }

    public void tuneCamera(float horizontal, float vertical)
    {
        this.bCameraDragAim.rotateHorizontal(horizontal);
        this.bCameraDragAim.rotateVertical(vertical);
    }

}
