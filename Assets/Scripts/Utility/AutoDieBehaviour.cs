using UnityEngine;
using System.Collections;

public class AutoDieBehaviour : MonoBehaviour
{
    [Tooltip("In seconds")]
    public float timeToLive = 5.0f;
    //private float birthday = 0;
	// Use this for initialization
	void Start () {
        // compute when to die
        //this.birthday = Time.time + this.timeToLive;
        //StartCoroutine("autoDieTrigger");

        // turns out there is a built-in api for it...
        Destroy(this.gameObject, this.timeToLive);
    }
	
    //IEnumerator autoDieTrigger()
    //{
    //    yield return new WaitForSeconds(this.timeToLive);
    //}
}
