using UnityEngine;
using System.Collections;

public class BEnemyAIChasePlayer : MonoBehaviour {

    public float changeDirectionInterval = 3;
    public float maxSpeed = 50;
    private Vector3 wanderDirection;

	// Use this for initialization
	void Start () {
        StartCoroutine("updateWanderDirection");

    }
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.AddForce(this.wanderDirection*100000);
        if (rb.velocity.magnitude > this.maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * this.maxSpeed;
        }
	}

    IEnumerator updateWanderDirection()
    {
        while (true)
        {
            this.wanderDirection = Random.onUnitSphere;
            yield return new WaitForSeconds(this.changeDirectionInterval);
        }
    }
}
