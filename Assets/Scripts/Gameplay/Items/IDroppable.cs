namespace Gameplay.Items
{
	public interface IDroppable
	{
		/// <summary>
		///     Drop an object on the ground.
		/// </summary>
		void Drop();

		/// <summary>
		///     Pick up an object from the ground.
		/// </summary>
		void PickUp();
	}
}