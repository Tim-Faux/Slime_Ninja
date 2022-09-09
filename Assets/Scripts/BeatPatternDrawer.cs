using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BeatPattern))]
public class BeatPatternDrawer : PropertyDrawer
{
	/*
	*	Creates the textbox to input the number of beats per letter
	*/
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		label = EditorGUI.BeginProperty(position, label, property);

		Rect contentPos = EditorGUI.PrefixLabel(position, label);
		// Sets the size of the textbox 
		if (position.height > 16f) {
			position.height = 16f;
			EditorGUI.indentLevel += 1;
			contentPos = EditorGUI.IndentedRect(position);
		}
		// Makes the textbox 75% the size to fit nicely within the window
		contentPos.width *= 0.75f; 
		EditorGUI.indentLevel = 0;

		// Draws the textbox
		MakeNumBeatsProperty(property, contentPos);

		EditorGUI.EndProperty();
	}

	/*
	 *	Draws the actual textbox
	 */
	private static void MakeNumBeatsProperty(SerializedProperty property, Rect contentPos)
	{
		EditorGUI.indentLevel = 0;
		contentPos.x += 15f;
		//Makes textbox wider
		contentPos.width *= 1.2f;
		EditorGUIUtility.labelWidth = 65f;
		EditorGUI.PropertyField(contentPos, property.FindPropertyRelative("NumBeats"), new GUIContent("Num Beats"));
	}
}
