// This is not an editor script. The property attribute class should be placed in a regular script file.
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class ReadmeAttribute : PropertyAttribute
{

    public UnityEditor.MessageType type;

    public ReadmeAttribute(UnityEditor.MessageType type)
    {
        this.type = type;
    }

    //public ReadmeAttribute() : this(UnityEditor.MessageType.Info) { 

    //}
}
#endif