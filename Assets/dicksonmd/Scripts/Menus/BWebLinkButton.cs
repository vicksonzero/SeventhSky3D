using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BWebLinkButton : MonoBehaviour
{

    public string link = "";

    private string _link = "";

    // Use this for initialization
    void Start()
    {
        this.GetComponentInChildren<Text>().text = link;
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

    public void go()
    {
        Application.OpenURL(link);
    }
}
