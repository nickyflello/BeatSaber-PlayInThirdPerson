using HarmonyLib;
using UnityEngine;

namespace PlayInThirdPerson.Harmony_Patches
{
	[HarmonyPatch(typeof(PlayerController), "Update")]
	class PlayerControllerUpdate
	{
		static void Postfix(PlayerController __instance)
		{
			Vector3 headPos = __instance.headPos - ConfigHelper.Config.Offset;
			ReflectionHelper.SetField(__instance, "_headPos", headPos);
		}
	}
}
