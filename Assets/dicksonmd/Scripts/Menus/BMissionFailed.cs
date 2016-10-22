using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BMissionFailed : MonoBehaviour {

    public RectTransform[] lines;
    public float secondsPerLine = 1;


    public BUIImageFadeIn bg;
    public GameObject failedScreenGroup;

    public bool sequenceStarted = false;
    public float timeToRestart = 6;

    public delegate void SequenceEndCallback();
    public SequenceEndCallback sequenceEndCallbacks;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (this.sequenceStarted)
        {
            timeToRestart -= Time.deltaTime;
            if (timeToRestart < 0)
            {
                if (sequenceEndCallbacks != null)
                {
                    sequenceEndCallbacks();
                }
            }
        }
	}

    public void startSequence()
    {
        this.bg.startToFade();
        this.failedScreenGroup.SetActive(true);
        this.sequenceStarted = true;
    }
}
