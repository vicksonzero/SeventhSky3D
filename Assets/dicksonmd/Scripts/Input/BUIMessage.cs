using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BUIMessage : MonoBehaviour {

    [Header("Config")]
    public bool testing = false;

    public bool isVisible = false;
    public float messageKeptTime = 5;
    public float fadeSpeed = 1;

    [Header("Linking")]
    public Text textfield;

    private CanvasGroup group;

	// Use this for initialization
	void Start () {
        this.group = this.GetComponent<CanvasGroup>();
        log("System started");
	}
	
	// Update is called once per frame
	void Update () {
        if (testing && Input.GetMouseButtonUp(0))
        {
            log("Click");
        }
        if (this.isVisible)
        {
            this.group.alpha = 1;
        }
        else
        {
            this.group.alpha = Mathf.Lerp(this.group.alpha, 0.001f, fadeSpeed * Time.deltaTime);
        }
    }

    public static void log(string msg)
    {
        var inst = GameObject.FindObjectOfType<BUIMessage>();
        inst.textfield.text += "\n" + msg;

        inst.isVisible = true;
        inst.StopAllCoroutines();
        inst.StartCoroutine(inst.delayedFade(inst.messageKeptTime));
    }
    
    IEnumerator delayedFade(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.isVisible = false;
    }
}
