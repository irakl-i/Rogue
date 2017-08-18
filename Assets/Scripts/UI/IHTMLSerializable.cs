using UnityEngine;

namespace UI
{
	public interface IHTMLSerializable
	{
		string ToHTML(Color nameColor, Color descriptionColor, Color valueColor);
	}
}