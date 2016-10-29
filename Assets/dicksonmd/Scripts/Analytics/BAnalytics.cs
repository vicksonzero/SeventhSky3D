using UnityEngine;
using System.Collections;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using UnityEngine.SceneManagement;

public class BAnalytics : MonoBehaviour {

#if UNITY_EDITOR
    [Readme(UnityEditor.MessageType.Info)]
    public string readme = "Analytics module\nHolds methods for making AJAX calls to analytics server. \nInterface for other analytics modules to submit data.";
#endif

    [Header("Fill in")]
    public bool debugAJAX = false;
    
    public string serverAddr = "http://localhost:3000/aeroplane1";

    //private string serverAddr = "http://api.openweathermap.org/data/2.5/weather?q=hongkong&appid=2eb1f7df7619778c5195aff70749146d";
    //http://api.openweathermap.org/data/2.5/weather?q=sheungwan,hk&appid=2eb1f7df7619778c5195aff70749146d

    [Header("For inspection only")]
    public string analyticsName = "";

    public bool gameStarted = false;


    public static BAnalytics i;
    void Awake()
    {
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        this.loadPref();

        if (! gameStarted && SceneManager.GetActiveScene().buildIndex == 0)
        {
            //print(this.get());
            if (analyticsName == "")
            {
                analyticsName = get("newPlayer");
            }
            if(debugAJAX) print(analyticsName);
            post("openApp");
        }
        gameStarted = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnApplicationQuit()
    {
        this.savePref();
    }

    string get(string eventString)
    {
        var encoding = new System.Text.UTF8Encoding();
        using (var client = new WebClient())
        {
            var url = this.serverAddr + "/" + eventString + "?" + "v=" + BPersistData.i.version;
            if(debugAJAX) print(url);
            var responseString = client.DownloadString(url);
            return responseString;
        }
    }

    void post(string eventString)
    {
        string url = this.serverAddr + "/" + eventString;
        if(debugAJAX) print(url);

        WWWForm form = new WWWForm();
        form.AddField("username", this.analyticsName);
        form.AddField("v", BPersistData.i.version);

        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
    }


    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            if(debugAJAX) Debug.Log("WWW Ok!: " + www.text);
        }
        else {
            if(debugAJAX) Debug.Log("WWW Error: " + www.error);
        }
    }

    void loadPref()
    {
        this.analyticsName = PlayerPrefs.GetString("AnalyticsName", "");
    }

    void savePref()
    {
        PlayerPrefs.SetString("AnalyticsName", this.analyticsName);
    }


    //string post()
    //{
    //    using (var client = new WebClient())
    //    {
    //        var values = new NameValueCollection();
    //        values["thing1"] = "hello";
    //        values["thing2"] = "world";

    //        var response = client.UploadValues(this.serverAddr, values);

    //        var responseString = Encoding.Default.GetString(response);
    //        return responseString;
    //    }
    //}
}
