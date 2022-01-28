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
    class GameState
    {
        internal List<GameObject> enemies { get; set; } = new List<GameObject>();
        internal ActionList actionList { get; set; } = new ActionList();
        internal EnemyList enemyList { get; set; } = new EnemyList();
        internal bool slyPhaseTwo { get; set; } = false;
        internal bool displayingUnknownParryText { get; set; } = false;



        internal bool hardMode { get; set; } = true;
    }
}
