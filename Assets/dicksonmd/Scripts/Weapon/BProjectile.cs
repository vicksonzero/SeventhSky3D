using UnityEngine;
using System.Collections;


public class BProjectile : MonoBehaviour {

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
        // if we have hit an enemy
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Vector3 hitpoint = col.contacts[0].point;
            col.gameObject.GetComponentInParent<BEnemy>().takeDamage(this.damage);
            Instantiate(this.firework, hitpoint, Quaternion.identity);

            Destroy(this.gameObject);
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("WeaponPhysical"))
        {
            Vector3 hitpoint = col.contacts[0].point;
            BUnit bunit = col.gameObject.GetComponentInParent<BUnit>();
            if (bunit) bunit.takeDamage(this.damage);
            Instantiate(this.firework, hitpoint, Quaternion.identity);

            Destroy(this.gameObject);
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Structures"))
        {
            Vector3 hitpoint = col.contacts[0].point;
            Instantiate(this.firework, hitpoint, Quaternion.identity);
            BUnit bunit = col.gameObject.GetComponentInParent<BUnit>();
            if(bunit) bunit.takeDamage(this.damage);
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

                Destroy(this.gameObject);
            }
            else
            {

                print("hit player");
                // Default

                Instantiate(this.firework, hitpoint, Quaternion.identity);

                Destroy(this.gameObject);
            }

        }
    }
}
