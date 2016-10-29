using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BLoadScene : MonoBehaviour {

    public string sceneName;

    public void load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
