using UnityEngine;
using System.Collections;

public class BEnemyLookAtPlayer : MonoBehaviour
{

    public Transform head;

    public float rotateSpeed = 20;

    public float updateInterval = 0;

    private BPlayer player;

    private Quaternion targetRotation;

    // Use this for initialization
    void Start()
    {
        this.player = GameObject.FindObjectOfType<BGameMaster>().player;
        this.targetRotation = this.transform.rotation;
        this.StartCoroutine(this.updateRotationCoroutine(this.updateInterval));
    }

    // Update is called once per frame
    void Update()
    {
        if (this.updateInterval <= 0)
        {
            updateTargetRotation();
        }

        var maxStep = rotateSpeed * Time.deltaTime;
        this.head.transform.rotation = Quaternion.RotateTowards(this.head.transform.rotation, this.targetRotation, maxStep);
    }

    private void updateTargetRotation()
    {
        var displacement = this.player.transform.position - this.head.transform.position;
        this.targetRotation = Quaternion.LookRotation(displacement, Vector3.up);
    }

    IEnumerator updateRotationCoroutine(float interval)
    {
        if (interval > 0)
        {
            while (true)
            {
                this.updateTargetRotation();
                yield return new WaitForSeconds(interval);
            }
        }
    }

}
