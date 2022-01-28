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
    class CharmHandler
    {
        // Disables damage dealt by Weaverlings
        internal static IntOperator HandleWeaverlingDamage(IntOperator self)
        {
            if (self.State.Name.Equals("Hit") || self.State.Name.Equals("Parent?") || self.State.Name.Equals("G Parent?"))
                self.integer2 = 0;
            return self;
        }

        // Disables damage dealt by Dream Shield
        internal static IntOperator HandleDreamShieldDamage(IntOperator self)
        {
            if (self.State.Name.Equals("Hit") || self.State.Name.Equals("Parent?") || self.State.Name.Equals("G Parent?"))
                self.integer2 = 0;
            return self;
        }

        // Disables the hatchlings dung cloud created from Glowing Womb x Defender's Crest
        internal static KnightHatchling HandleDungHatchlingExplosion(KnightHatchling self)
        {
            self.dungExplosionPrefab.SetActive(false);
            return self;
        }

        // Disables fluke damage from Flukenest 
        internal static SpellFluke HandleSpellFlukeDamage(SpellFluke self)
        {
            FieldInfo damage = ReflectionHelper.GetField<SpellFluke, FieldInfo>(self, "damage");
            damage.SetValue(self, 0);
            return self;
        }

        // Disables the damaging cloud created when focusing with Sporeshroom along with Defender's Crest
        internal static PlayMakerFSM HandleFocusCloud(PlayMakerFSM self)
        {
            FsmUtil.RemoveAction(self.GetState("Spore Cloud"), 3);
            FsmUtil.RemoveAction(self.GetState("Dung Cloud"), 0);
            FsmUtil.RemoveAction(self.GetState("Spore Cloud 2"), 3);
            FsmUtil.RemoveAction(self.GetState("Dung Cloud 2"), 0);
            return self;
        }

        // Disables the damaging dung trail left behind the Knight from Defender's Crest
        internal static PlayMakerFSM HandleDungTrail(PlayMakerFSM self)
        {
            FsmUtil.RemoveTransition(self.GetState("Emit Pause"), "Equipped");
            return self;
        }

        // Disable's Grimmchild's fireball attack
        internal static PlayMakerFSM HandleGrimmchildShot(PlayMakerFSM self)
        {
            self.GetAction<GameObjectIsNull>("Check For Target", 2).isNotNull = self.GetAction<GameObjectIsNull>("Check For Target", 2).isNull;
            return self;
        }

        // Disables Dung Fluke's cloud explosion from Flukenest x Defender's Crest
        internal static PlayMakerFSM HandleSpellFlukeDung(PlayMakerFSM self)
        {
            switch (self.gameObject.name)
            {
                case "Spell Fluke Dung Lv2":
                    self.GetAction<Wait>("Blow", 11).time = 1;
                    FsmUtil.RemoveAction(self.GetState("Blow"), 10);
                    break;
                default:
                    self.GetAction<Wait>("Blow", 9).time = 1;
                    FsmUtil.RemoveAction(self.GetState("Blow"), 8);
                    break;
            }
            return self;
        }
    }
}
