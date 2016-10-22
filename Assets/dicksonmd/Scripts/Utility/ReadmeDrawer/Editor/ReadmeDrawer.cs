// The property drawer class should be placed in an editor script, inside a folder called Editor.

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadmeAttribute))]
public class ReadmeDrawer : PropertyDrawer
{

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        // ReadmeAttribute contains the `type` of the helpbox
        ReadmeAttribute readme = attribute as ReadmeAttribute;

        EditorGUI.BeginProperty(position, label, property);

        // label of the readme property. usually "readme"
        EditorGUI.LabelField(position, label.text,EditorStyles.boldLabel);

        // calculate the height of the label
        GUIStyle myStyle = new GUIStyle(EditorStyles.label);
        Vector2 sizeOfLabel = myStyle.CalcSize(new GUIContent(label.text));

        // area for the helpbox
        var amountRect = new Rect(position.x, position.y + sizeOfLabel.y, position.width, position.height - sizeOfLabel.y);


        // The whole point of this script
        EditorGUI.HelpBox(amountRect, property.stringValue, readme.type);

        EditorGUI.EndProperty();

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // size of helpbox
        GUIStyle myStyle = new GUIStyle(EditorStyles.helpBox);
        Vector2 sizeOfLabel = myStyle.CalcSize(new GUIContent(property.stringValue));

        // size of helpbox plus size of label (?)
        return base.GetPropertyHeight(property, label) + sizeOfLabel.y;
        //return sizeOfLabel.y + 10;
    }
}