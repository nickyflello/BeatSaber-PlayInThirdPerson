using UnityEngine;

namespace PlayInThirdPerson
{
	class CameraMover : MonoBehaviour
	{
		Vector3 CameraOffset => ConfigHelper.Config.Offset;

		private void LateUpdate()
		{
			// Add offset to camera
			if (Plugin.IsEnabled)
			{
				transform.localPosition = CameraOffset;
			}
			else
			{
				transform.localPosition = Vector3.zero;
			}
		}
	}
}
