using System;
using System.Collections;
using System.Collections.Generic;
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
    partial class ParryKnight : IGlobalSettings<CustomGlobalSaveData>
    {
        public static CustomGlobalSaveData GlobalSaveData { get; set; } = new CustomGlobalSaveData();

        public void OnLoadGlobal(CustomGlobalSaveData s)
        {
            GlobalSaveData = s ?? GlobalSaveData ?? new();
        }

        public CustomGlobalSaveData OnSaveGlobal()
        {
            return GlobalSaveData;
        }
    }

    public class CustomGlobalSaveData
    {
        public enum DIFFICULTY_SETTING { APPRENTICE, MASTER, SAGE };
        public DIFFICULTY_SETTING difficulty;

        public double pantheonBoost;
        public bool enabled;
    }
}
