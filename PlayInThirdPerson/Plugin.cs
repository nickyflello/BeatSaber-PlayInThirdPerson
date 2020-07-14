using BeatSaberMarkupLanguage.Settings;
using HarmonyLib;
using IPA;
using PlayInThirdPerson.UI;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayInThirdPerson
{
	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin
	{
		public static bool IsEnabled => ConfigHelper.Config.Enabled && !IsPlayingReplay;

		private static bool IsPlayingReplay;

		private void SetupCamera()
		{
			Transform mainCamera = Camera.main.transform;
			Transform cameraMover = new GameObject("Camera Mover").transform;
			cameraMover.SetParent(mainCamera.parent, false);
			cameraMover.gameObject.AddComponent<CameraMover>();
			mainCamera.SetParent(cameraMover, true);
		}

		private void MenuSceneLoaded()
		{
			IsPlayingReplay = false;
		}

		private void LateMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
		{
			SetupCamera();
		}

		private void GameSceneLoaded()
		{
			try
			{
				// Find the replay player and check if we are in a replay.
				// We don't have access to the assembly info here, so use reflection and string comparisons.
				Component replayPlayer =
					(GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[])
					.First(obj => obj.name.Contains("ReplayPlayer"))
					.GetComponents<Component>()
					.First(c => c.GetType().Name.Contains("ReplayPlayer"));

				IsPlayingReplay = ReflectionHelper.GetField<bool>(replayPlayer, "#=ziUDn9LCmOc4NDTUp$WvNusk="); // Cool field name.
				Console.WriteLine($"[PlayInThirdPerson] IsPlayingReplay: {IsPlayingReplay}");
			}
			catch (Exception e)
			{
				Console.WriteLine("[PlayInThirdPerson] Failed to check replay game state, asumming non-replay");
				Console.WriteLine(e);
				IsPlayingReplay = false;
			}

			if (IsEnabled)
			{
				SetupCamera();
			}
		}

		[OnStart]
		public void OnApplicationStart()
		{
			try
			{
				Harmony harmony = new Harmony("com.Nicky.BeatSaber.PlayInThirdPerson");
				harmony.PatchAll(Assembly.GetExecutingAssembly());
			}
			catch (Exception e)
			{
				Console.WriteLine("[PlayInThirdPerson] This plugin requires Harmony.");
				Console.WriteLine(e);
			}

			BS_Utils.Utilities.BSEvents.OnLoad();
			BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh += LateMenuSceneLoadedFresh;
			BS_Utils.Utilities.BSEvents.menuSceneLoaded += MenuSceneLoaded; ;
			BS_Utils.Utilities.BSEvents.gameSceneLoaded += GameSceneLoaded;
			SceneManager.activeSceneChanged += OnActiveSceneChanged;
			ConfigHelper.LoadConfig();
		}

		[OnExit]
		public void OnApplicationQuit()
		{
			BS_Utils.Utilities.BSEvents.gameSceneLoaded -= GameSceneLoaded;
			BS_Utils.Utilities.BSEvents.menuSceneLoaded -= MenuSceneLoaded; ;
			BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh -= LateMenuSceneLoadedFresh;
		}

		public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
		{
			if (nextScene.name == "MenuViewControllers" && prevScene.name == "EmptyTransition")
			{
				BSMLSettings.instance.AddSettingsMenu("Third Person", "PlayInThirdPerson.UI.SettingsUI.bsml", SettingsUI.instance);
			}
		}
	}
}
