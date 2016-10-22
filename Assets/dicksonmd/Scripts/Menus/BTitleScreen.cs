using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BTitleScreen : MonoBehaviour {

#if UNITY_EDITOR
    [Readme(UnityEditor.MessageType.Info)]
    public string readme = "Controller that maps strings with canvas elements, \n and respond to button presses";
#endif
    public string nextScreenName;
    public Text VersionLabel;
    public Text DateLabel;


    // Use this for initialization
    void Start ()
    {
        VersionLabel.text = "Version " + BPersistData.i.version;
        DateLabel.text = BPersistData.i.lastChangedDate;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void startGame()
    {
        SceneManager.LoadScene(nextScreenName);
    }
}
