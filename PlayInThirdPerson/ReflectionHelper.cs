using System.Reflection;

namespace PlayInThirdPerson
{
	public static class ReflectionHelper
	{
		private static BindingFlags NonPublicFlags => BindingFlags.Instance | BindingFlags.NonPublic;

		public static T GetField<T>(object obj, string fieldName)
			=> (T)obj.GetType().GetField(fieldName, NonPublicFlags).GetValue(obj);

		public static void SetField<T>(object obj, string fieldName, T value)
			=> obj.GetType().GetField(fieldName, NonPublicFlags).SetValue(obj, value);

		public static T GetProperty<T>(object obj, string PropertyName)
			=> (T)obj.GetType().GetProperty(PropertyName, NonPublicFlags).GetValue(obj);

		public static void SetProperty<T>(object obj, string PropertyName, T value)
			=> obj.GetType().GetProperty(PropertyName, NonPublicFlags).SetValue(obj, value);
	}
}
