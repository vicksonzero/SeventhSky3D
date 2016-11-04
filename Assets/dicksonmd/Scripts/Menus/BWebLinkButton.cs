using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BWebLinkButton : MonoBehaviour
{

    public string link = "";
    public string linkText = "";

    // Use this for initialization
    void Start()
    {
        this.GetComponentInChildren<Text>().text = (linkText == "" ? link : linkText);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setLink(string val)
    {
        link = val;
        this.GetComponentInChildren<Text>().text = link;
    }

    public void setLink(string val, string linkText)
    {
        if (val != null)
        {
            link = val;
        }
        if (linkText != null)
        {
            this.GetComponentInChildren<Text>().text = linkText;
        }
    }

    public void go()
    {
        Application.OpenURL(link);
    }
}
