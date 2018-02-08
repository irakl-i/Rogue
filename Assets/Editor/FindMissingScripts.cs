/*
 *	Created on 6/23/2017 9:02:57 PM
 *	Project Stealth by Irakli
 */

using UnityEditor;
using UnityEngine;

public class FindMissingScripts : EditorWindow
{
	[MenuItem("Window/Find Missing Scripts")]
	public static void ShowWindow()
	{
		GetWindow(typeof(FindMissingScripts));
	}

	public void OnGUI()
	{
		if (GUILayout.Button("Find Missing Scripts in selected prefabs"))
			FindInSelected();
	}

	private static void FindInSelected()
	{
		var go = Selection.gameObjects;
		int goCount = 0, componentsCount = 0, missingCount = 0;
		foreach (GameObject g in go)
		{
			goCount++;
			var components = g.GetComponents<Component>();
			for (var i = 0; i < components.Length; i++)
			{
				componentsCount++;
				if (components[i] == null)
				{
					missingCount++;
					var s = g.name;
					Transform t = g.transform;
					while (t.parent != null)
					{
						s = t.parent.name + "/" + s;
						t = t.parent;
					}
					Debug.Log(s + " has an empty script attached in position: " + i, g);
				}
			}
		}

		Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", goCount, componentsCount,
			missingCount));
	}
}