using UnityEngine;
using System.Collections;

public class BProjectile : MonoBehaviour {

    public Transform firework;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Instantiate(this.firework, this.transform.position, Quaternion.identity);

        }
    }
}
