using System.Collections;
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

		public static Vector2 Clamp(this Vector2 vector)
		{
			return new Vector2((int) vector.x, (int) vector.y);
		}

		public static Vector3 Clamp(this Vector3 vector)
		{
			return new Vector3((int) vector.x, (int) vector.y, (int) vector.z);
		}

		public static void Pause()
		{
			Time.timeScale = 0;
		}

		public static void Unpause()
		{
			Time.timeScale = 1;
		}
	}
}