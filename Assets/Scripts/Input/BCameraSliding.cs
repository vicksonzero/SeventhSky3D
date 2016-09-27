using UnityEngine;
using System.Collections;

public class BCameraSliding : MonoBehaviour {

    [Header("Fill in")]
    public Rigidbody player;
    [Tooltip("Camera that moves according to player speed")]
    public Camera mainCamera;
    public float maxSpeed = 500;
    public float maxSlide = -0.2f;
    public float cameraSlideSpeed = 1;
    public float FOVChangeSpeed = 2;

    public float maxFOVChange = 40;


    [Header("private, just for inspection")]
    public Vector3 startPosition;
    public float maxSpeedSq;
    public float startFieldOfView;

    // Use this for initialization
    void Start ()
    {
        this.startPosition = this.mainCamera.transform.localPosition;
        this.maxSpeedSq = this.maxSpeed * this.maxSpeed;
        this.startFieldOfView = this.mainCamera.fieldOfView;
    }
	
	// Update is called once per frame
	void Update () {
        float offset = playerSpeedToCameraOffset(player.GetComponent<Rigidbody>());
        // move camera for speed
        //this.mainCamera.transform.localPosition = Vector3.Lerp(
        //    this.mainCamera.transform.localPosition,
        //    this.startPosition + new Vector3(0, 0, offset * this.maxSlide),
        //    Time.deltaTime * this.cameraSlideSpeed);
        float targetFOV = this.startFieldOfView + offset * this.maxFOVChange;
        this.mainCamera.fieldOfView = Mathf.Lerp(this.mainCamera.fieldOfView, targetFOV, Time.deltaTime * this.FOVChangeSpeed);
    }
    public float playerSpeedToCameraOffset(Rigidbody playerRB)
    {
        return Mathf.Clamp(playerRB.velocity.sqrMagnitude / this.maxSpeedSq, 0, 1);//
    }
}
