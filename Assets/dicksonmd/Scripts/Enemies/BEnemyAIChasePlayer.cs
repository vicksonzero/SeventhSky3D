using UnityEngine;
using System.Collections;

public class BEnemyAIChasePlayer : MonoBehaviour {

    public float changeDirectionInterval = 5;
    public float maxSpeed = 50;

    public float distFromPlayer = 400;
    public float distFromPlayerNoise = 50;

    private float distFromPlayerSq;
    private Vector3 destination;
    private BPlayer player;

	// Use this for initialization
	void Start () {
        this.player = Object.FindObjectOfType<BGameMaster>().player;
        this.distFromPlayerSq = this.distFromPlayer * this.distFromPlayer;
        StartCoroutine(updateDestination());
    }
	
	void FixedUpdate () {
        float step = this.maxSpeed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.destination, step);
    }

    IEnumerator updateDestination()
    {
        while (true)
        {
            var displacementFromPlayer = this.transform.position - this.player.transform.position;
            var distSq = displacementFromPlayer.sqrMagnitude;
            //if (distSq > this.distFromPlayerSq)
            //{
            this.destination = (
                this.player.transform.position +
                displacementFromPlayer.normalized * this.distFromPlayer +
                Random.insideUnitSphere * Random.Range(0, this.distFromPlayerNoise)
                );
            //}
            //this.destination = Random.insideUnitSphere * (this.distFromPlayer + Random.Range(0, this.distFromPlayerNoise));
            yield return new WaitForSeconds(this.changeDirectionInterval);
        }
    }
}
