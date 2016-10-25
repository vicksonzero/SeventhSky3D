using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class BEnemyMissile : BProjectile
{

    public float rotateSpeed = 1;

    public float targetAngle = 5;
    public BUnit target;

    public float noise = 2;

    // Use this for initialization
    void Start()
    {
        GameObject.FindObjectOfType<BUISound>().playMissileAlert();
        StartCoroutine(updateTarget());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (this.target != null)
        {
            var targetVector = (this.target.transform.position + Random.insideUnitSphere * noise) - this.transform.position;

            var newRotation = Quaternion.Slerp(
                this.transform.rotation,
                Quaternion.LookRotation(targetVector, this.transform.up),
                Mathf.Clamp01(this.rotateSpeed * Time.deltaTime)
                );
            var rb = this.GetComponent<Rigidbody>();

            rb.MoveRotation(newRotation);
            rb.velocity = this.transform.forward * rb.velocity.magnitude;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        print("some collision happened");
        // if we have hit an enemy
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Vector3 hitpoint = col.contacts[0].point;
            //Instantiate(this.firework, hitpoint, Quaternion.identity);

            //Destroy(this.gameObject);
        }

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
            if (shield != null)
            {
                print("hit shield");
                Instantiate(this.firework, hitpoint, Quaternion.identity);

                Destroy(this.gameObject);
            }
            else
            {

                print("hit player");
                col.gameObject.GetComponentInParent<BUnit>().takeDamage(this.damage);

                Instantiate(this.firework, hitpoint, Quaternion.identity);

                Destroy(this.gameObject);
            }

        }
    }

    IEnumerator updateTarget()
    {
        while (true)
        {
            // ditch target only if out of chase angle
            if (this.target != null)
            {
                var travellingAngle = this.GetComponent<Rigidbody>().velocity;
                var angle = Vector3.Angle(
                    travellingAngle,
                    this.target.transform.position - this.transform.position
                    );
                if (angle < this.targetAngle)
                {
                    this.target = null;
                }
            }

            // get a new target if target lost
            if (this.target == null)
            {
                var availableTargets =
                    GameObject.FindObjectsOfType<BPlayer>().ToList()
                    .Cast<BUnit>().ToList();

                this.target = this.closestInAngle(availableTargets);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    BUnit closestInAngle(List<BUnit> availableTargets)
    {

        var maxAngle = this.targetAngle;
        var travellingAngle = this.GetComponent<Rigidbody>().velocity;//this.transform.rotation.eulerAngles;
        var pos = this.transform.position;

        BUnit result = null;
        var length = availableTargets.Count;
        for (int i = 0; i < length; i++)
        {
            var angle = Vector3.Angle(
                travellingAngle,
                availableTargets[i].transform.position - pos
                );
            if (angle < maxAngle)
            {
                result = availableTargets[i];
                maxAngle = angle;
            }
        }
        return result;
    }

}
