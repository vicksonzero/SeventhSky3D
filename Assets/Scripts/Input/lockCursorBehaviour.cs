using UnityEngine;
using System.Collections;

public class lockCursorBehaviour : MonoBehaviour
{
#region [private variables]
#endregion

#region [Unity Events]
    // Use this for initialization
    void Start () {
        Debug.Log("lockCursorBehaviour loaded");
        Screen.lockCursor = true;
        Debug.Log("cursor is now " + (Screen.lockCursor ? "Locked" : "Free"));
	}
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Screen.lockCursor = !Screen.lockCursor;
            Debug.Log("cursor is now " + (Screen.lockCursor ? "Locked" : "Free"));
        }
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("click screen and press P to lock cursor again");
        }
    }
#endregion

#region [private method]
#endregion
    
}
