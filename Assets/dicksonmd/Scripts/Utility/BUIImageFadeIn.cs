using UnityEngine;
using System.Collections;

public class BUIImageFadeIn : MonoBehaviour {

    private UnityEngine.UI.Image img;
    public float fadeSpeed = 1;
    public bool isfading = false;

	// Use this for initialization
	void Start () {
        this.img = this.GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.isfading)
        {
            Color c = this.img.color;
            c.a = Mathf.Lerp(c.a, 1, Time.deltaTime * this.fadeSpeed);
            this.img.color = c;
        }
	}
    public void startToFade()
    {
        this.isfading = true;
    }
}