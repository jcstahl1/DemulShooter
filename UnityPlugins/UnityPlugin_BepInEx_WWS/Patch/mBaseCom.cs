﻿using HarmonyLib;

namespace WildWestShootout_BepInEx_DemulShooter_Plugin
{
    class mBaseCom
    {
        /// <summary>
        /// Removing the call to the dog_winfows_ dll causing a crash when it's called with admin rights (netdll broken messagebox)
        /// </summary>
        [HarmonyPatch(typeof(BaseCom), "CheckDog")]
        class CheckDog
        {
            static bool Prefix()
            {
                UnityEngine.Debug.LogError("mBaseCom.CheckDog()");
                return false;
            }
        }
    }
}
