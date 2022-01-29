using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;

namespace ParryKnight
{
    class GameState
    {
        internal List<GameObject> enemies { get; set; } = new List<GameObject>();
        internal ActionList actionList { get; set; } = new ActionList();
        internal EnemyList enemyList { get; set; } = new EnemyList();
        internal bool slyPhaseTwo { get; set; } = false;
        internal bool displayingUnknownParryText { get; set; } = false;
    }
}
