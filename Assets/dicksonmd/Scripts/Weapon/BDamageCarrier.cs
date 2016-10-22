using UnityEngine;
using System.Collections.Generic;

public class BDamageCarrier : MonoBehaviour {

    public GameObject parent; // TODO: change to a generic "entity" class with damage and hp

    public float damage = 0;

    public List<float> absorbedDamage;

    public Transform succeedFirework;

}
