// ***********************************************************
// Written by Heyworks Unity Studio http://unity.heyworks.com/
// ***********************************************************
using UnityEngine;
/// <summary>
/// Gyroscope controller that works with any device orientation.
/// </summary>
public class GyroController : MonoBehaviour
{
	#region [Private fields]

		private bool gyroEnabled = true;
		private const float lowPassFilterFactor = 0.2f;

		private readonly Quaternion baseIdentity = Quaternion.Euler (90, 0, 0);
		private readonly Quaternion landscapeRight = Quaternion.Euler (0, 0, 90);
		private readonly Quaternion landscapeLeft = Quaternion.Euler (0, 0, -90);
		private readonly Quaternion upsideDown = Quaternion.Euler (0, 0, 180);
	
		private Quaternion cameraBase = Quaternion.identity;
		private Quaternion calibration = Quaternion.identity;
		private Quaternion baseOrientation = Quaternion.Euler (90, 0, 0);
		private Quaternion baseOrientationRotationFix = Quaternion.identity;
		private Quaternion referanceRotation = Quaternion.identity;
		private bool debug = true;
		private GameObject RotateObjectGame;

	#endregion

	#region [Unity events]

		protected void Start ()
		{
			//	RotateObjectGame = gameObject;
				Input.gyro.enabled = true;
				AttachGyro ();
		}

		protected void Update ()
		{
				if (!gyroEnabled)
						return;
				transform.localRotation = Quaternion.Slerp (transform.localRotation,
		                                       cameraBase * (ConvertRotation (referanceRotation * Input.gyro.attitude) * GetRotFix ()), lowPassFilterFactor);

				//RotateObjectGame.transform.localEulerAngles = new Vector3 (0, 0, transform.localEulerAngles.y);
		}



	#endregion

	#region [Public methods]

		/// <summary>
		/// Attaches gyro controller to the transform.
		/// </summary>
		private void AttachGyro ()
		{
				gyroEnabled = true;
				ResetBaseOrientation ();
				UpdateCalibration (true);
				UpdateCameraBaseRotation (true);
				RecalculateReferenceRotation ();
		}

		/// <summary>
		/// Detaches gyro controller from the transform
		/// </summary>
		private void DetachGyro ()
		{
				gyroEnabled = false;
		}

	#endregion

	#region [Private methods]

		/// <summary>
		/// Update the gyro calibration.
		/// </summary>
		private void UpdateCalibration (bool onlyHorizontal)
		{
				if (onlyHorizontal) {
						var fw = (Input.gyro.attitude) * (-Vector3.forward);
						fw.z = 0;
						if (fw == Vector3.zero) {
								calibration = Quaternion.identity;
						} else {
								calibration = (Quaternion.FromToRotation (baseOrientationRotationFix * Vector3.up, fw));
						}
				} else {
						calibration = Input.gyro.attitude;
				}
		}
	
		/// <summary>
		/// Update the camera base localRotation.
		/// </summary>
		/// <param name='onlyHorizontal'>
		/// Only y localRotation.
		/// </param>
		private void UpdateCameraBaseRotation (bool onlyHorizontal)
		{
				if (onlyHorizontal) {
						var fw = transform.forward;
						fw.y = 0;
						if (fw == Vector3.zero) {
								cameraBase = Quaternion.identity;
						} else {
								cameraBase = Quaternion.FromToRotation (Vector3.forward, fw);
						}
				} else {
						cameraBase = transform.localRotation;
				}
		}
	
		/// <summary>
		/// Converts the localRotation from right handed to left handed.
		/// </summary>
		/// <returns>
		/// The result localRotation.
		/// </returns>
		/// <param name='q'>
		/// The localRotation to convert.
		/// </param>
		private static Quaternion ConvertRotation (Quaternion q)
		{
				return new Quaternion (q.x, q.y, -q.z, -q.w);	
		}
	
		/// <summary>
		/// Gets the rot fix for different orientations.
		/// </summary>
		/// <returns>
		/// The rot fix.
		/// </returns>
		private Quaternion GetRotFix ()
		{
#if UNITY_3_5
		if (Screen.orientation == ScreenOrientation.Portrait)
			return Quaternion.identity;
		
		if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.Landscape)
			return landscapeLeft;
				
		if (Screen.orientation == ScreenOrientation.LandscapeRight)
			return landscapeRight;
				
		if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
			return upsideDown;
		return Quaternion.identity;
#else
				return Quaternion.identity;
#endif
		}
	
		/// <summary>
		/// Recalculates reference system.
		/// </summary>
		private void ResetBaseOrientation ()
		{
				baseOrientationRotationFix = GetRotFix ();
				baseOrientation = baseOrientationRotationFix * baseIdentity;
		}

		/// <summary>
		/// Recalculates reference localRotation.
		/// </summary>
		private void RecalculateReferenceRotation ()
		{
				referanceRotation = Quaternion.Inverse (baseOrientation) * Quaternion.Inverse (calibration);
		}

	#endregion
}
