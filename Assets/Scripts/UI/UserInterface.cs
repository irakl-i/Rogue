/*
 *	Created on 8/7/2017 7:45:16 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using Gameplay.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UserInterface : MonoBehaviour
	{
		[SerializeField]
		private Player player;

		private Text health;

		private void Start()
		{
			health = GetComponent<Text>();
		}

		private void Update()
		{
			health.text = player.Health.ToString();
		}
	}
}