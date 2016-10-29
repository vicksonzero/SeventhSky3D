using UnityEngine;
using System.Collections;


public class BPersistData : MonoBehaviour {
#if UNITY_EDITOR
    [Readme(UnityEditor.MessageType.Warning)]
    public string readme = "Persistent data object for shared data. Can start alone.\n\nDon't change value in inspector\nChange in prefab.";
#endif
    public string version = "pre_2.15";
    public string lastChangedDate = "2016-10-13";

    public static BPersistData i;
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

	//// Use this for initialization
	//void Start ()
 //   {

 //   }
	
	//// Update is called once per frame
	//void Update () {
	
	//}


}
