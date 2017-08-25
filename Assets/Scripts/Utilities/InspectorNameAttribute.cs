using UnityEngine;

namespace Utilities
{
	public class InspectorNameAttribute : PropertyAttribute
	{
		public string name;

		public InspectorNameAttribute(string name)
		{
			this.name = name;
		}
	}
}