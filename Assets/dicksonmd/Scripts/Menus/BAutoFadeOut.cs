using UnityEngine;
using System.Collections;

public class BAutoFadeOut : MonoBehaviour {

    public float delay = 2;

    public float fadeLength = 1;

    private bool isFading = false;

    private float animEndTime = 0;

    private CanvasGroup group;

	// Use this for initialization
	void Start () {
        group = this.GetComponent<CanvasGroup>();
        StartCoroutine(this.scheduleFade());
	}
	
	// Update is called once per frame
	void Update () {
        if (this.isFading)
        {
            this.group.alpha = (animEndTime - Time.time) / this.fadeLength;
            if(this.group.alpha <= 0)
            {
                Destroy(this.gameObject);
            }
        }
	}

    void startFade()
    {
        animEndTime = Time.time + this.fadeLength;
        this.isFading = true;
    }

    IEnumerator scheduleFade()
    {
        yield return new WaitForSeconds(this.delay);
        startFade();
    }
}
