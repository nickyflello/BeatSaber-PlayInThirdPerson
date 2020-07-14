using BeatSaberMarkupLanguage.Attributes;

namespace PlayInThirdPerson.UI
{
	class SettingsUI : PersistentSingleton<SettingsUI>
	{
		private Config _tempConfig = new Config(ConfigHelper.Config);

		[UIValue("boolEnable")]
		public bool Enabled
		{
			get => _tempConfig.Enabled;
			set => _tempConfig.Enabled = value;
		}

		[UIValue("cameraOffsetY")]
		public float CameraOffsetY
		{
			get => _tempConfig.Offset.Y;
			set => _tempConfig.Offset.Y = value;
		}

		[UIValue("cameraOffsetZ")]
		public float CameraOffsetZ
		{
			get => _tempConfig.Offset.Z;
			set => _tempConfig.Offset.Z = value;
		}

		[UIAction("#apply")]
		public void OnApply()
		{
			ConfigHelper.SaveNewConfig(_tempConfig);
		}

		[UIAction("#ok")]
		public void OnOk()
		{
			ConfigHelper.SaveNewConfig(_tempConfig);
		}
	}
}
