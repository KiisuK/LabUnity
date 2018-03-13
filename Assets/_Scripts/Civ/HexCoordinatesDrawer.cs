using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HexCoordinates))]
public class HexCoordinatesDrawer : PropertyDrawer {

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		HexCoordinates coordinates = new HexCoordinates (property.FindPropertyRelative ("x").intValue, property.FindPropertyRelative ("z").intValue);

		position = EditorGUI.PrefixLabel (position, label);
		GUI.Label (position, coordinates.ToString ());
	}

	public static HexCoordinates FromPosition(Vector3 position){
		float x = position.x / (HexMetrics.innerRadius * 2f);
		float y = -x;

		float offset = position.z / (HexMetrics.outerRadius * 3f);
		x -= offset;
		y -= offset;

		int iX = Mathf.RoundToInt (x);
		int iY = Mathf.RoundToInt (y);
		int iZ = Mathf.RoundToInt (-x-y);

		return new HexCoordinates (iX, iZ);
	}
}
