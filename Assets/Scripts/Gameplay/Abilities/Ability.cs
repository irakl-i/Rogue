using UnityEngine;

namespace Gameplay.Abilities
{
	public interface IAbility
	{
		int ID { get; }
		string Name { get; }

		void Use();
	}
}