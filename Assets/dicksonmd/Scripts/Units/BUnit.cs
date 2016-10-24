using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BUnit : MonoBehaviour
{

    [Header("Unit")]
    public float hp = 100;
    public float maxhp = 100;

    public enum Team { player, enemy };
    
    public Team team;

    public Transform dieFirework;

    public delegate void DamageTakenDelegate();
    public event DamageTakenDelegate hpChanged;

    public bool isInvincible = false;


    // Use this for initialization
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void takeDamage(float amount)
    {
        this.hp -= amount;

        if (this.hp <= 0)
        {
            this.die();
        }

        if (this.hp > this.maxhp)
        {
            this.hp = maxhp;
        }

        this.onHPChanged();
    }

    protected virtual void onHPChanged()
    {
        if (this.hpChanged != null)
        {
            this.hpChanged();
        }
    }

    public float getHPPercent()
    {
        return this.hp / this.maxhp;
    }

    public virtual void die()
    {
        if (this.dieFirework) { Instantiate(this.dieFirework, this.transform.position, Quaternion.identity); }
        this.remove();
    }

    public virtual void remove()
    {
        //print("remove()");

        Destroy(this.gameObject);
    }

    public void copyFrom(BUnit other)
    {
        this.hp = other.hp;
        this.maxhp = other.maxhp;

        this.dieFirework = other.dieFirework;

        this.hpChanged = other.hpChanged;
    }


    public List<BUnit> unitsWithin(List<BUnit> availableTargets, float maxDist)
    {
        var maxDistSq = maxDist * maxDist;

        var result = new List<BUnit>();
        var length = availableTargets.Count;
        for (int i = 0; i < length; i++)
        {
            var distSq = (this.transform.position - availableTargets[i].transform.position).sqrMagnitude;
            if (distSq <= maxDistSq)
            {
                result.Add(availableTargets[i]);
            }
        }
        return result;
    }

    public BUnit unitClosest(List<BUnit> availableTargets)
    {
        return BUnit.unitClosestTo(this.transform, availableTargets);
    }


    public static BUnit unitClosestTo(Transform thisarg, List<BUnit> availableTargets)
    {
        var maxDistSq = Mathf.Infinity;

        BUnit result = null;
        var length = availableTargets.Count;
        for (int i = 0; i < length; i++)
        {
            var distSq = (
                thisarg.transform.position -
                availableTargets[i].transform.position
                ).sqrMagnitude;

            if (distSq < maxDistSq)
            {
                result = availableTargets[i];
                maxDistSq = distSq;
            }
        }
        return result;
    }
}
