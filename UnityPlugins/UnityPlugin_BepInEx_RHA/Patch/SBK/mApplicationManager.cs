﻿using System;
using HarmonyLib;
using Microsoft.Win32;
using UnityEngine;

namespace RabbidsHollywood_BepInEx_DemulShooter_Plugin
{
    class mApplicationManager
    {
        /// <summary>
        /// The original function restarts the game if the fullscreen is disabled
        /// And it's overriding FullScreen option in the registry, forcing it to 1 again
        /// </summary>
        [HarmonyPatch(typeof(SBK.ApplicationManager), "Awake")]
        class Awake
        {
            static void Postfix()
            {
                DemulShooter_Plugin.MyLogger.LogMessage("mApplicationManager.Awake(), Screen=" + Screen.width + ", " + Screen.height + "," + Screen.fullScreen);
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Sarbakan\\RabbidsShooter");
                    if (key != null)
                    {
                        System.Object o = key.GetValue("Screenmanager Is Fullscreen mode_h3981298716");
                        if (o != null)
                        {
                            if ((int)o == 1)
                                Screen.SetResolution(Screen.width, Screen.height, true);
                            else if ((int)o == 0)
                                Screen.SetResolution(Screen.width, Screen.height, false);
                            DemulShooter_Plugin.MyLogger.LogMessage("mApplicationManager.Awake(), FullScreen now is " + Screen.fullScreen);                
                        }
                        else
                        {
                            DemulShooter_Plugin.MyLogger.LogMessage("mApplicationManager.Awake() : Can't find Registry Value : HK_CURRENT_USER\\SOFTWARE\\Sarbakan\\RabbidsShooter\\Screenmanager Is Fullscreen mode_h3981298716");
                        }
                    }
                    else
                    {
                        DemulShooter_Plugin.MyLogger.LogMessage("mApplicationManager.Awake() : Can't find Registry Key : HK_CURRENT_USER\\SOFTWARE\\Sarbakan\\RabbidsShooter");
                    }
                }
                catch (Exception Ex)
                {
                    DemulShooter_Plugin.MyLogger.LogMessage("mApplicationManager.Awake() : Error reading fullscreen registry value. " + Ex.Message.ToString());
                }
            }
        }
    }
}
