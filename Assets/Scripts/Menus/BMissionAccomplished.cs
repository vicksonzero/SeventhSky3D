using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BMissionAccomplished : MonoBehaviour {

    public RectTransform[] lines;
    public float secondsPerLine = 1;


    public BUIImageFadeIn bg;
    public GameObject accomplishedScreenGroup;

    public bool sequenceStarted = false;
    public float timeToRestart = 6;
    public string sceneName;

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
                Application.LoadLevel(this.sceneName);
            }
        }
	}

    public void startSequence()
    {
        this.bg.startToFade();
        this.accomplishedScreenGroup.SetActive(true);
        this.sequenceStarted = true;
    }
}
