using UnityEditor;
using UnityEngine;

namespace Gamelogic.Extensions.Editor
{
	/// <summary>
	///     A property drawer for fields marked with the CommentAttribute. Similar to Header, but useful
	///     for longer descriptions.
	/// </summary>
	[CustomPropertyDrawer(typeof(CommentAttribute), true)]
	public class CommentPropertyDrawer : DecoratorDrawer
	{
		private CommentAttribute CommentAttribute => (CommentAttribute) attribute;

		public override float GetHeight()
		{
			return EditorStyles.whiteLabel.CalcHeight(CommentAttribute.content, Screen.width - 19);
		}

		public override void OnGUI(Rect position)
		{
			EditorGUI.BeginDisabledGroup(true);
			EditorGUI.LabelField(position, CommentAttribute.content);
			EditorGUI.EndDisabledGroup();
		}
	}
}