using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using GlobalEnums;
using Modding;
using UnityEngine;
using UnityEngine.UI;
using Mono.Cecil.Cil;
using MonoMod;
using TMPro;
using UnityEngine.SceneManagement;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Language;
using Vasi;

namespace ParryKnight
{
    class Tools
    {
        private static Dictionary<string, double> bossMultiplier = new Dictionary<string, double>()
        {
            { "Grimm Boss", 2.5 },
            { "Nightmare Grimm Boss", 2.5 },
            { "Hornet Boss 2", 2 },
            { "Hive Knight", 2 },
            { "Lost Kin", 2 },
            { "Sheo Boss", 1.5 },
            { "HK Prime", 1.5 },
            { "Infected Knight", 1.5 },
            { "Black Knight 1", 1.5 },
            { "Black Knight 2", 1.5 },
            { "Black Knight 3", 1.5 },
            { "Black Knight 4", 1.5 },
            { "Black Knight 5", 1.5 },
            { "Black Knight 6", 1.5 },
        };

        internal static double getBossMultiplier(string key, GameState gameState)
        {
            double multiplier = 1;
            if (BossSequenceController.IsInSequence && !gameState.hardMode)
                multiplier *= 2;

            if (!bossMultiplier.Keys.Contains(key))
                return multiplier;
            return multiplier * bossMultiplier[key];
        }

        internal static string CleanName(string name)
        {
            if (name.Contains("("))
                name = name.Substring(0, name.IndexOf("(")).Trim();
            return name;
        }

        internal static bool isEnemyParryable(string name, GameState gameState)
        {
            return gameState.enemyList.parryableEnemies.Contains(name) ||
                (name.Equals("Sly Boss") && !gameState.slyPhaseTwo);
        }

        internal static bool isActionParryable(string name, string state, GameState gameState)
        {
            return gameState.actionList.parryableActions.Contains(name + "-" + state) ||
                        name.Equals("Mawlek Body") ||
                        name.Equals("Mawlek Col");
        }

        // Allow damage exception for Aspids and False Knight before obtaining a spell
        internal static bool isDamagingProgressionEnemy(string name)
        {
            return PlayerData.instance.fireballLevel == 0 && (
                    name.Equals("Spitter") || // Aspids (to escape the Aspid Arena)
                    name.Equals("False Knight New") || // False Knight, and his Head
                    name.Equals("Head"));
        }

        internal static void displayUnknownParryText(GameState gameState)
        {
            gameState.displayingUnknownParryText = true;
            GameObject c = CanvasUtil.CreateCanvas(RenderMode.ScreenSpaceOverlay, new Vector2(1920, 1080));
            Text ohNo = CanvasUtil.CreateTextPanel(c, "OH NO! You found a parry the mod dev didn't know about!",
                35, TextAnchor.MiddleCenter, new CanvasUtil.RectData(
                new Vector2(1500, 1500), new Vector2(0f, 70), new Vector2(0.5f, 0), new Vector2(0.5f, 0), new Vector2(0.5f, 0.5f)
            ), true).GetComponent<Text>();
            Text sendLog = CanvasUtil.CreateTextPanel(c, "After closing the game, send your ModLog to AngelSassin in the HK Discord.",
                25, TextAnchor.MiddleCenter, new CanvasUtil.RectData(
                new Vector2(1500, 1500), new Vector2(0f, 30), new Vector2(0.5f, 0), new Vector2(0.5f, 0), new Vector2(0.5f, 0.5f)
            ), true).GetComponent<Text>();
        }
    }
}
