/*
 *	Created on 8/21/2017 11:19:49 PM
 *	Project Rogue by Irakli Chkuaseli
 */

using UnityEngine;
using UnityEngine.Assertions;

namespace Camera
{
	public class PixelPerfectCamera : MonoBehaviour
	{
		[SerializeField]
		private int pixelsPerUnit = 16;

		[SerializeField]
		private int verticalUnitsOnScreen = 4;

		private new UnityEngine.Camera camera;
		public float FinalUnitSize { get; private set; }

		public int PixelsPerUnit => pixelsPerUnit;
		public int VertUnitsOnScreen => verticalUnitsOnScreen;

		private void Awake()
		{
			camera = gameObject.GetComponent<UnityEngine.Camera>();
			Assert.IsNotNull(camera);

			SetOrthographicSize();
		}

		private void SetOrthographicSize()
		{
			ValidateUserInput();

			// get device's screen height and divide by the number of units 
			// that we want to fit on the screen vertically. this gets us
			// the basic size of a unit on the the current device's screen.
			var tempUnitSize = Screen.height / verticalUnitsOnScreen;

			// with a basic rough unit size in-hand, we now round it to the
			// nearest power of pixelsPerUnit (ex; 16px.) this will guarantee
			// our sprites are pixel perfect, as they can now be evenly divided
			// into our final device's screen height.
			FinalUnitSize = GetNearestMultiple(tempUnitSize, pixelsPerUnit);

			// ultimately, we are using the standard pixel art formula for 
			// orthographic cameras, but approaching it from the view of:
			// how many standard Unity units do we want to fit on the screen?
			// formula: cameraSize = ScreenHeight / (DesiredSizeOfUnit * 2)
			camera.orthographicSize = Screen.height / (FinalUnitSize * 2.0f);
		}

		private int GetNearestMultiple(int value, int multiple)
		{
			var rem = value % multiple;
			var result = value - rem;
			if (rem > multiple / 2)
				result += multiple;

			return result;
		}

		private void ValidateUserInput()
		{
			if (pixelsPerUnit == 0)
			{
				pixelsPerUnit = 1;
				Debug.Log("Warning: Pixels-per-unit must be greater than zero. " +
				          "Resetting to minimum allowed.");
			}
			else if (verticalUnitsOnScreen == 0)
			{
				verticalUnitsOnScreen = 1;
				Debug.Log("Warning: Units-on-screen must be greater than zero." +
				          "Resetting to minimum allowed.");
			}
		}
	}
}