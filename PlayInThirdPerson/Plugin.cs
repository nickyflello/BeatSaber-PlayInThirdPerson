using HarmonyLib;
using IPA;
using System;
using System.Reflection;
using UnityEngine;

namespace PlayInThirdPerson
{
	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin
	{
		private void SetupCamera()
		{
			Transform mainCamera = Camera.main.transform;
			Transform t = new GameObject("Camera Mover").transform;
			t.SetParent(mainCamera.parent, false);
			t.gameObject.AddComponent<CameraMover>();
			mainCamera.SetParent(t, true);
		}

		private void LateMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
		{
			SetupCamera();
		}

		private void GameSceneLoaded()
		{
			SetupCamera();
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
			BS_Utils.Utilities.BSEvents.gameSceneLoaded += GameSceneLoaded;
			ConfigHelper.LoadConfig();
		}

		[OnExit]
		public void OnApplicationQuit()
		{
			BS_Utils.Utilities.BSEvents.gameSceneLoaded -= GameSceneLoaded;
			BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh -= LateMenuSceneLoadedFresh;
		}
	}
}
