using UnityEngine;
// no using directive needed
public class TitleReadme : MonoBehaviour
{

    [TextArea(4, 50)]
    public string readme = "" +
        "This scene is game Entry point.\n" +
        "\n" +
        "_GlobalData is persistent\n" +
        "Main Camera contains transitional scipts\n" +
        "Canvas contains ui management scripts\n" +
        "";
}