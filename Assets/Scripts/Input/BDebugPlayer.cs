using UnityEngine;
using System.Collections;

public class BDebugPlayer : MonoBehaviour {

    public lockCursorBehaviour lockCursorBehaviour;
    public rotateWMouseBehaviour rotateWMouseBehaviour;

    // Use this for initialization
    void Start () {

#if UNITY_EDITOR
        this.lockCursorBehaviour.enabled = true;
        this.rotateWMouseBehaviour.enabled = true;
#endif
    }

    // Update is called once per frame
    void Update () {
	
	}
}
