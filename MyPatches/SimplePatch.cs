using EFT;
using Comfort.Common;
using System.Reflection;
using HarmonyLib;
using SPT.Reflection.Patching;


namespace makeMedsGreatAgain.MyPatches
{
    internal class SimplePatch : ModulePatch // all patches must inherit ModulePatch
    {
 
        internal struct PlayerInfo
        {
            internal static GameWorld gameWorld
            { get => Singleton<GameWorld>.Instance; }

            internal static Player.FirearmController FC
            { get => player.HandsController as Player.FirearmController; }

            internal static Player player
            { get => gameWorld.MainPlayer; }

        }

        
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(MovementContext), nameof(MovementContext.SetPhysicalCondition));
        }

        [PatchPrefix]
        static bool Prefix(MovementContext __instance, EPhysicalCondition c, ref bool __result)
        {
            // code in Prefix() method will run BEFORE original code is executed.
            // if 'true' is returned, the original code will still run.
            // if 'false' is returned, the original code will be skipped.

            if (c == EPhysicalCondition.HealingLegs && Plugin.canWalkInSurgery.Value)
            {
                __result = false; 
                return false; 
            }

            return true; 

        }

    }

}
