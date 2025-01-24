using EFT;
using System.Reflection;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace makeMedsGreatAgain.MyPatches
{
    internal class runPatch : ModulePatch // all patches must inherit ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(MovementContext), nameof(MovementContext.SetPhysicalCondition));
        }

        [PatchPrefix]
        static bool Prefix(EPhysicalCondition c, ref bool __result, Player ____player)
        {
            // If this player instance is not the main player, don't continue the rest of the method
            if (!____player.IsYourPlayer || PlayerInfo.player is HideoutPlayer)
            {
                return true;
            }
            
            Plugin.LogSource.LogWarning("_PLAYER => " + ____player.Profile.Info.Nickname);

            if (c == EPhysicalCondition.UsingMeds && Plugin.canSprintUsingMeds.Value)
            {
                __result = false;
                return false;
                
            }

            return true;
        }
    }
}
