
namespace PlayInThirdPerson
{
	public class Config
	{
		public struct Vector3
		{
			public Vector3(float x, float y, float z)
			{
				X = x;
				Y = y;
				Z = z;
			}

			public static implicit operator UnityEngine.Vector3(Vector3 v)
				=> new UnityEngine.Vector3(v.X, v.Y, v.Z);

			public float X;
			public float Y;
			public float Z;
		}

		public Vector3 Offset = new Vector3(0f, 0f, -1f);
	}
}
