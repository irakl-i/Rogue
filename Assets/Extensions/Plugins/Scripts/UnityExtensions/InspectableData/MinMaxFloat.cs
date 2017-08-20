// Copyright Gamelogic (c) http://www.gamelogic.co.za

using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Extensions
{
	/// <summary>
	///     Class for representing a bounded range.
	/// </summary>
	[Version(1, 2)]
	[Serializable]
	public class MinMaxFloat
	{
		public MinMaxFloat()
		{
			min = 0.0f;
			max = 1.0f;
		}

		public MinMaxFloat(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		#region Public Fields

		public float min;
		public float max = 1.0f;

		#endregion
	}
}