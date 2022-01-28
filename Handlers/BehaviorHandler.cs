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
    class BehaviorHandler
    {
        // If Broken Vessel or Lost Kin are staggered, don't spawn balloons
        internal static GameObject HandleBrokenVesselBalloons(GameObject arg)
        {
            string name = Tools.CleanName(arg.name);
            if (!name.Equals("Parasite Balloon Spawner"))
                return arg;

            GameObject boss = GameObject.Find("Lost Kin");
            if (!boss)
                boss = GameObject.Find("Infected Knight");
            if (!boss)
                return arg;

            PlayMakerFSM fsm = boss.GetComponent<PlayMakerFSM>();
            if (!fsm)
                return arg;

            string state = fsm.ActiveStateName;
            if (state.Equals("Stunned")) 
            {
                arg.Recycle();
                return null;
            }
            return arg;
        }

        // Disables killing the shade with dream nail
        internal static PlayMakerFSM HandleDreamNailShade(PlayMakerFSM self)
        {
            FsmUtil.RemoveAction(self.GetState("Idle"), 0);
            FsmUtil.RemoveTransition(self.GetState("Idle"), "Die");
            return self;
        }
    }
}
