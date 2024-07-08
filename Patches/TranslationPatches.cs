using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWRichPresence.Patches
{
	[HarmonyPatch(typeof(TextTranslation))]
	public static class TranslationPatches
	{
		[HarmonyPostfix]
		[HarmonyPatch(nameof(TextTranslation.InitializeLanguage))]
		public static void InitializeLanguage(TextTranslation __instance)
		{
			OWRichPresence.Instance.InitializeTranslationWithReload();
		}
	}
}
