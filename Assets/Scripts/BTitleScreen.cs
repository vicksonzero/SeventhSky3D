using UnityEngine;
using System.Collections;

public class BTitleScreen : MonoBehaviour {

    public string nextScreenName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void startGame()
    {
        Application.LoadLevel(nextScreenName);
    }
}
