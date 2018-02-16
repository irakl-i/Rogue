using Gameplay.Items.Inventory;
using UnityEngine;

namespace Gameplay.Managers
{
    public class GameManager : MonoBehaviour {
        private void Start()
        {
            Inventory.Instance.Initialize();
        }
    }
}
