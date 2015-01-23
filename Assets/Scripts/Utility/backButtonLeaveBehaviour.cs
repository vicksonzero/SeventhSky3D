using UnityEngine;
using System.Collections;

public class backButtonLeaveBehaviour : MonoBehaviour {

    public enum BackTo { ExitGame, nextScene };
    public BackTo onPressBackButton;
    public string sceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (onPressBackButton == BackTo.ExitGame)
            {
                Application.Quit();
            }
            else
            {
                Application.LoadLevel(sceneName);
            }
        }
	}
}
