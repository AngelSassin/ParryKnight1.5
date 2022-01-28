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
    public class ParryKnight : Mod
    {
        public override string GetVersion() => "0.1.4.0";
        internal static ParryKnight Instance;
        private GameState gameState = new GameState();

        public override void Initialize()
        {
            Instance = this;
            Log("Initializing");

            On.HeroController.NailParry += OnNailParry;
            On.HealthManager.TakeDamage += OnTakeDamage;
            On.PlayMakerFSM.OnEnable += OnFsmEnable;
            On.SpellFluke.DoDamage += OnFlukeDamage;
            On.KnightHatchling.Spawn += OnHatchlingSpawn;
            On.HutongGames.PlayMaker.Actions.IntOperator.OnEnter += OnIntOperator;
            ModHooks.ObjectPoolSpawnHook += OnObjectPoolSpawn;
            ModHooks.OnEnableEnemyHook += EnemyEnabled;
            ModHooks.BeforeSceneLoadHook += BeforeSceneLoad;
            ModHooks.LanguageGetHook += LanguageGet;
        }

        private void OnNailParry(On.HeroController.orig_NailParry orig, HeroController self)
        {
            bool parryOccurred = false;
            List<string> logActions = new List<string>();

            foreach (GameObject enemy in gameState.enemies)
            {
                if (!enemy)
                    continue;
                PlayMakerFSM fsm = enemy.GetComponent<PlayMakerFSM>();
                if (!fsm)
                    continue;

                string state = fsm.ActiveStateName;
                string name = Tools.CleanName(enemy.name);
                logActions.Add("PARRY     " + name + "     " + name + "-" + state);
                if (Tools.isActionParryable(name, state, gameState))
                {
                    parryOccurred = true;
                    HealthManager enemyHealth = enemy.GetComponent<HealthManager>();
                    ParryKnightHandler.HandleParryDamage(name, enemyHealth, gameState);
                    if (name.Equals("Sly Boss") && enemyHealth.hp <= 0)
                        gameState.slyPhaseTwo = true;
                }
            }

            if (!parryOccurred)
            {
                foreach (string log in logActions)
                    Log("NOT LOGGED!!! " + log);
                if (!gameState.displayingUnknownParryText)
                    Tools.displayUnknownParryText(gameState);
            }

            orig(self);
        }

        private void OnTakeDamage(On.HealthManager.orig_TakeDamage orig, HealthManager self, HitInstance hitInstance)
        {
            string name = Tools.CleanName(self.name);
            if (!hitInstance.Source)
            {
                hitInstance.DamageDealt = 0;
                orig(self, hitInstance);
                return;
            }

            if (Tools.isEnemyParryable(name, gameState))
                hitInstance = ParryKnightHandler.HandleParryableEnemyDamage(hitInstance);
            else
            {
                if (Tools.isDamagingProgressionEnemy(name)) 
                {
                    orig(self, hitInstance);
                    return;
                }
                hitInstance = ParryKnightHandler.HandleNonParryableEnemyDamage(hitInstance);
            }
            
            if (hitInstance.AttackType.Equals(AttackTypes.Nail) && (
                    hitInstance.Source.name.Equals("Hit L") ||
                    hitInstance.Source.name.Equals("Hit R") ||
                    hitInstance.Source.name.Equals("Great Slash") ||
                    hitInstance.Source.name.Equals("Dash Slash")))
                ParryKnightHandler.HandleNailArtSoulGain(hitInstance);

            if (hitInstance.AttackType.Equals(AttackTypes.Generic))
            {
                if (hitInstance.Source.name.Equals("Damager"))
                    hitInstance = ParryKnightHandler.HandleGlowingWombDamage(hitInstance);

                if (hitInstance.Source.name.Equals("Hit U") ||
                        hitInstance.Source.name.Equals("Hit D") ||
                        hitInstance.Source.name.Equals("Hit L") ||
                        hitInstance.Source.name.Equals("Hit R"))
                    hitInstance = ParryKnightHandler.HandleThornsDamage(hitInstance);
                
                if (hitInstance.Source.name.Equals("SuperDash Damage") || hitInstance.Source.name.Equals("SD Burst"))
                    hitInstance = ParryKnightHandler.HandleCrystalDashDamage(hitInstance);
            }
            
            if (name.Equals("Parasite Balloon Spawner")) 
                self = ParryKnightHandler.HandleParasiteBalloonHit(self, hitInstance);

            if (hitInstance.DamageDealt != 0)
                Log("HIT LOGGED: " + hitInstance.AttackType + "    " + name + "    " + hitInstance.Source);

            if (name.Equals("Sly Boss") && self.hp <= 0)
                gameState.slyPhaseTwo = true;

            orig(self, hitInstance);
        }

        private void OnFsmEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            string name = Tools.CleanName(self.gameObject.name);

            if (name == "Hollow Shade" && self.FsmName == "Dreamnail Kill") 
                self = BehaviorHandler.HandleDreamNailShade(self);
            
            if (name == "Knight" && self.FsmName == "Spell Control") 
                self = CharmHandler.HandleFocusCloud(self);

            if (name == "Dung" && self.FsmName == "Control")
                self = CharmHandler.HandleDungTrail(self);

            if (name == "Grimmchild" && self.FsmName == "Control")
                self = CharmHandler.HandleGrimmchildShot(self);

            if (name.Contains("Spell Fluke Dung") && self.FsmName == "Control")
                self = CharmHandler.HandleSpellFlukeDung(self);

            orig(self);
        }

        private void OnFlukeDamage(On.SpellFluke.orig_DoDamage orig, SpellFluke self, GameObject obj, int recursion, bool burst)
        {
            string name = Tools.CleanName(obj.name);
            if (Tools.isEnemyParryable(name, gameState))
                self = CharmHandler.HandleSpellFlukeDamage(self);
            orig(self, obj, recursion, burst);
        }

        private System.Collections.IEnumerator OnHatchlingSpawn(On.KnightHatchling.orig_Spawn orig, KnightHatchling self)
        {
            return orig(CharmHandler.HandleDungHatchlingExplosion(self));
        }

        private void OnIntOperator(On.HutongGames.PlayMaker.Actions.IntOperator.orig_OnEnter orig, IntOperator self)
        {
            if (self.Fsm.GameObject.name.Equals("Enemy Damager") && self.Fsm.Name.Equals("Attack"))
                self = CharmHandler.HandleWeaverlingDamage(self);

            if (self.Fsm.GameObject.name.Equals("Shield") && self.Fsm.Name.Equals("Shield Hit"))
                self = CharmHandler.HandleDreamShieldDamage(self);

            orig(self);
        }

        private GameObject OnObjectPoolSpawn(GameObject arg)
        {
            return BehaviorHandler.HandleBrokenVesselBalloons(arg);
        }

        public bool EnemyEnabled(GameObject enemy, bool isDead)
        {
            if (isDead)
                return isDead;
            gameState.enemies.Add(enemy);
            return false;
        }

        public string BeforeSceneLoad(string sceneName)
        {
            gameState.enemies.Clear();
            gameState.displayingUnknownParryText = false;
            gameState.slyPhaseTwo = false;
            return sceneName;
        }

        public string LanguageGet(string key, string sheet, string orig)
        {
            Log(key + ": " + Language.Language.GetInternal(key, sheet));

            string value = TextReplacements.Get(key);
            if (value == null)
                return orig;
            return value;
        }
    }
}
