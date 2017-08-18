using UnityEngine;

namespace Utilities
{
	public static class Extensions
	{
		public static string ToHex(this Color color)
		{
			Color32 color32 = color;
			var hex = color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2");
			return hex;
		}
	}
}