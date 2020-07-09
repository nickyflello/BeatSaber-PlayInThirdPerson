using UnityEngine;

namespace PlayInThirdPerson
{
	class CameraMover : MonoBehaviour
	{
		Vector3 CameraOffset => ConfigHelper.Config.Offset;

		private void Update()
		{
			// TODO: Move to confighelper
			if (Input.GetKeyDown(KeyCode.F5))
			{
				ConfigHelper.LoadConfig();
			}
		}

		private void LateUpdate()
		{
			// Add offset to camera
			transform.localPosition = CameraOffset;
		}
	}
}
