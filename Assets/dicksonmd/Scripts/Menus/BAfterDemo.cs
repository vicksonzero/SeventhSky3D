using UnityEngine;
using System.Collections;

public class BAfterDemo : MonoBehaviour {

    public string prefillLink = "https://docs.google.com/forms/d/e/1FAIpQLSffIhUDkYanWBbUh9MhhQBqzP9tTdRucrokIkORkPSQPyM-Vg/viewform?entry.206283932=TESTING";

    public string replaceHandle = "TESTING";

	// Use this for initialization
	void Start () {
        var filledLink = prefillLink.Replace(replaceHandle, "RESERVED");
        var button = FindObjectOfType<BWebLinkButton>();
        button.setLink(filledLink, null);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
