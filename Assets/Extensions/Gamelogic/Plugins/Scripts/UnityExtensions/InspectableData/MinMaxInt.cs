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
	public class MinMaxInt
	{
		public MinMaxInt()
		{
			min = 0;
			max = 1;
		}

		public MinMaxInt(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		#region Public Fields

		public int min;
		public int max = 1;

		#endregion
	}
}