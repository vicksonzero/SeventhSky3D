using UnityEngine;
using System.Collections;

public class BShield : MonoBehaviour {

    public Collider shieldBody;

	// Use this for initialization
	void Start () {
        this.GetComponentInParent<BPlayer>().registerShields(this);
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
