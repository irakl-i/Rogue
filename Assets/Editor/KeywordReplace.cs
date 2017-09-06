using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class KeywordReplace : UnityEditor.AssetModificationProcessor
{
	public static void OnWillCreateAsset(string path)
	{
		path = path.Replace(".meta", "");
		var index = path.LastIndexOf(".", StringComparison.Ordinal);
		string file;
		try
		{
			file = path.Substring(index);
		}
		catch (ArgumentOutOfRangeException)
		{
			return;
		}
		if (!file.Contains(".cs")) return;
		index = Application.dataPath.LastIndexOf("Assets", StringComparison.Ordinal);
		path = Application.dataPath.Substring(0, index) + path;
		file = File.ReadAllText(path);
		var splitPath = path.Split('/');
		var folder = splitPath[splitPath.Length - 2];

		file = file.Replace("#NAMESPACE#", folder);
		file = file.Replace("#DEVELOPERS#", PlayerSettings.companyName);

		File.WriteAllText(path, file);
		AssetDatabase.Refresh();
	}
}