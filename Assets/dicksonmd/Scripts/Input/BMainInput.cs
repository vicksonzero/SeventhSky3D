using UnityEngine;
using System.Collections;
using System;

public class BMainInput : MonoBehaviour {

    [Header("Fill in")]
    public BPlayer player;
    public float dashInterval = 0.3f;
    public BCameraDragAim bCameraDragAim;

    //[Header("private, just for inspection")]
    private float dashCountdown = 0;
    private float dashCount = 0;


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


        if (Input.GetButtonDown("Weapon01"))
        {
            this.changeToWeapon(1);
        }

        if (Input.GetButtonDown("Weapon02"))
        {
            this.changeToWeapon(2);
        }

        if (Input.GetButtonDown("Weapon03"))
        {
            this.changeToWeapon(3);
        }

        if (Input.GetButtonDown("Screenshot"))
        {
            this.takeScreenshot();
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
    public bool weaponTryShoot(int index = -1)
    {
        this.updatePlayerRotation();
        return this.player.weaponTryShoot(index);
    }

    public bool changeToWeapon(int index)
    {
        return this.player.changeToWeapon(index);
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

    /// <summary>
    /// Rotates camera relatively by `horizontal` and `vertical` amounts. Please do your sensitivity outside.
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    public void tuneCamera(float horizontal, float vertical)
    {
        this.bCameraDragAim.rotateHorizontal(horizontal);
        this.bCameraDragAim.rotateVertical(vertical);
    }

    public void takeScreenshot()
    {
        string dateString = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
        print(Application.persistentDataPath);
        Application.CaptureScreenshot("screenshots/Screenshot " + dateString + ".png");
    }

}
