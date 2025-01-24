using EFT;
using Comfort.Common;
using System.Reflection;
using HarmonyLib;
using SPT.Reflection.Patching;


namespace makeMedsGreatAgain.MyPatches
{
    
    internal partial struct PlayerInfo
    {
        internal static GameWorld gameWorld
        {
            get => Singleton<GameWorld>.Instance;
        }

        internal static Player.FirearmController FC
        {
            get => player.HandsController as Player.FirearmController;
        }
        
        internal static Player.MedsController medsController
        {
            get => player.HandsController as Player.MedsController;
        }

        internal static Player player
        {
            get => gameWorld.MainPlayer;
        }

        internal static Player.ItemHandsController itemHandsController
        {
            get => player.HandsController as Player.ItemHandsController;
        }

        internal static Player.UsableItemController usableItemController
        {
            get => itemHandsController as Player.UsableItemController;
        }
    }
    
    internal class surgeryPatch : ModulePatch // all patches must inherit ModulePatch
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
            
            if (c == EPhysicalCondition.HealingLegs && Plugin.canWalkInSurgery.Value)
            {
                __result = false;
                return false;
            }
            return true;

        }
    }
}
