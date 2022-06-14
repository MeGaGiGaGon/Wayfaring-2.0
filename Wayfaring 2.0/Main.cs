using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Wayfaring_2._0
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    [BepInDependency(R2API.R2API.PluginGUID, R2API.R2API.PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string ModGuid = "com.MyUsername.MyModName";
        public const string ModName = "My Mod's Title and if we see this exact name on Thunderstore we will deprecate your mod";
        public const string ModVer = "0.0.1";

        public static ConfigEntry<int> Myconfig { get; set; }
        private void Awake()
        {
            Myconfig = Config.Bind<int>("Config", "StagesPerLoop", 5, "How many stages before the game start including loop content.");

            On.RoR2.Run.Start += (orig, self) =>
            {
                RoR2.Run.stagesPerLoop = Myconfig.Value;
                orig(self);
            };

            On.RoR2.Run.PickNextStageScene += RunOnPickNextStageScene;
            private static void RunOnPickNextStageScene(On.RoR2.Run.orig_PickNextStageScene orig, RoR2.Run self, WeightedSelection<RoR2.SceneDef> choices)
            {
                orig(self, choices);
            }
        }
    }
}
