using HarmonyLib;
using UnityEngine;

namespace PlayInThirdPerson.Harmony_Patches
{
	[HarmonyPatch(typeof(PlayerTransforms), "Update")]
	class PlayerTransformsUpdate
	{
		static void Postfix(PlayerTransforms __instance)
		{
			if (Plugin.IsEnabled)
			{
				Vector3 headPos = __instance.headPseudoLocalPos - ConfigHelper.Config.Offset;
				ReflectionHelper.SetField(__instance, "_headPseudoLocalPos", headPos);
			}
		}
	}
}
