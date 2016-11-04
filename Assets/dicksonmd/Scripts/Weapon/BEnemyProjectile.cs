using UnityEngine;
using System.Collections;


public class BEnemyProjectile : MonoBehaviour {

    public Transform firework;

    public float damage = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        print("some collision happened");

        if (col.gameObject.layer == LayerMask.NameToLayer("Structures"))
        {
            Vector3 hitpoint = col.contacts[0].point;
            Instantiate(this.firework, hitpoint, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            Vector3 hitpoint = col.contacts[0].point;

            var shield = col.gameObject.GetComponent<BShield>();
            if(shield != null)
            {
                print("hit shield");
                Instantiate(this.firework, hitpoint, Quaternion.identity);

                BAnalyticsGA.logUseShield();

                Destroy(this.gameObject);
            }
            else
            {

                print("hit player");
                // Default

                Instantiate(this.firework, hitpoint, Quaternion.identity);
                col.gameObject.GetComponent<BPlayer>().takeDamage(this.damage);
                Destroy(this.gameObject);
            }

        }
    }
}
