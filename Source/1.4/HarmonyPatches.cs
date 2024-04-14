using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace DisableRimworldVersionCorner
{
    [StaticConstructorOnStartup]
    internal class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("katana.disablerimworldversioncorner");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            var harmonymod = typeof(VersionControl).GetMethod("DrawInfoInCorner");
            harmony.Unpatch(harmonymod, HarmonyPatchType.Postfix, "net.pardeike.rimworld.lib.harmony");

            Log.Message("[DisableRimworldVersionCorner] Loaded");
        }

        [HarmonyPatch(typeof(VersionControl))]
        [HarmonyPatch(nameof(VersionControl.DrawInfoInCorner))]
        public static class VersionControl_DrawInfoInCorner_Patch
        {
            [HarmonyPrefix]
            public static bool Prefix()
            {
                return false;
            }
        }
    }
}
