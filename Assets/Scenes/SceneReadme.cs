using UnityEngine;
// no using directive needed
public class SceneReadme : MonoBehaviour
{
    [TextArea(4,50)]
    public string readme = "\n"+ 
        "This scene can start by itself.\n" + 
        "\n" +
        "Project folder/Scene/Title.unity is game's entry point.\n" +
        "\n" +
        "_GlobalData is persistent.\n" +
        "_GameMaster controls game flow, and is this room's entry point.\n" +
        "_Analytics calls AJAX server for statistics.\n" +
        "\n" +
        "UI/* folder holds ui, canvas and 2d cameras.\n" +
        " - control scripts attached to root canvas.\n" +
        " - 3D radar, however, lies in Stage/*\n" +
        "\n" +
        "Stage/* holds things that can move.\n" +
        " - Main Camera inside Stage/MainInput/Main Camera.\n" +
        " - other 3d cameras also here.\n" +
        "\n" +
        "__Workspace__ is tagged EditorOnly, and will delete itself when game starts.\n";
}