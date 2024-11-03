using EFT;
using Comfort.Common;
using System.Reflection;
using HarmonyLib;
using SPT.Reflection.Patching;


namespace makeMedsGreatAgain.MyPatches
{
    internal class SimplePatch : ModulePatch // all patches must inherit ModulePatch
    {
 
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(MovementContext), nameof(MovementContext.SetPhysicalCondition));
        }

        [PatchPrefix]
        static bool Prefix(EPhysicalCondition c, ref bool __result)
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
