using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParryKnight
{
    class ParryKnightHandler
    {
        // Applies damage on parry depending on the enemy and the game state
        internal static void HandleParryDamage(string name, HealthManager enemyHealth, GameState gameState)
        {
            if (!enemyHealth)
                return;
            int damage = PlayerData.instance.nailDamage;
            if (PlayerData.instance.GetBool("equippedCharm_25")) // If Strength Charm is equipped
                damage = (int)((damage * 1.5) + 0.5);
            if (PlayerData.instance.GetBool("equippedCharm_6") && PlayerData.instance.health == 1) // If Fury of the Fallen is equipped and hp = 1
                damage = (int)(damage * 1.75);
            double bossMultiplier = Tools.getBossMultiplier(name, gameState);
            if (ParryKnight.GlobalSaveData.difficulty == CustomGlobalSaveData.DIFFICULTY_SETTING.APPRENTICE)
                bossMultiplier *= 1.5;
            if (ParryKnight.GlobalSaveData.difficulty == CustomGlobalSaveData.DIFFICULTY_SETTING.MASTER)
                bossMultiplier *= 1.25;
            enemyHealth.ApplyExtraDamage((int)(damage * bossMultiplier));
        }

        // Disables non-parry damage dealt to parryable enemies
        internal static HitInstance HandleParryableEnemyDamage(HitInstance hitInstance)
        {
            if (hitInstance.AttackType.Equals(AttackTypes.Nail) || hitInstance.AttackType.Equals(AttackTypes.NailBeam))
                hitInstance.DamageDealt = 0;
            if (hitInstance.AttackType.Equals(AttackTypes.SharpShadow) || hitInstance.AttackType.Equals(AttackTypes.Spell))
                hitInstance.DamageDealt = 0;
            return hitInstance;
        }

        // Disables non-spell damage dealt to non-parryable enemies
        internal static HitInstance HandleNonParryableEnemyDamage(HitInstance hitInstance)
        {
            if (hitInstance.AttackType.Equals(AttackTypes.Nail) || hitInstance.AttackType.Equals(AttackTypes.NailBeam))
                hitInstance.DamageDealt = 0;
            if (hitInstance.AttackType.Equals(AttackTypes.SharpShadow))
                hitInstance.DamageDealt = 0;
            return hitInstance;
        }

        internal static void HandleParrySoulGain()
        {
            double additionalCharge = 1;
            switch (ParryKnight.GlobalSaveData.difficulty)
            {
                case CustomGlobalSaveData.DIFFICULTY_SETTING.APPRENTICE:
                    additionalCharge = 1;
                    break;
                case CustomGlobalSaveData.DIFFICULTY_SETTING.MASTER:
                    additionalCharge = 0.5;
                    break;
                case CustomGlobalSaveData.DIFFICULTY_SETTING.SAGE:
                    return;
            }
            additionalCharge *= GetBaseExtraSoul();

            PlayerData.instance.AddMPCharge((int) additionalCharge);

            // Update visuals
            GameCameras.instance.soulOrbFSM.SendEvent("MP GAIN");
            if (PlayerData.instance.GetInt("MPReserve") != 0)
                GameManager.instance.soulVessel_fsm.SendEvent("MP RESERVE UP");
        }

        // Applies extra soul gain when using Nail Arts
        internal static void HandleNailArtSoulGain(HitInstance hitInstance)
        {
            int additionalCharge = GetBaseExtraSoul();

            if (hitInstance.Source.name.Equals("Hit L") || hitInstance.Source.name.Equals("Hit R"))
                additionalCharge /= 2; // Nerf Cyclone Slash
            if (PlayerData.instance.GetBool("equippedCharm_26"))
                additionalCharge *= 2; // If Nailmaster's Glory is equipped, Gain even more additional soul on Nail Art.

            PlayerData.instance.AddMPCharge(additionalCharge);

            // Update visuals
            GameCameras.instance.soulOrbFSM.SendEvent("MP GAIN");
            if (PlayerData.instance.GetInt("MPReserve") != 0)
                GameManager.instance.soulVessel_fsm.SendEvent("MP RESERVE UP");
        }

        private static int GetBaseExtraSoul()
        {
            int tmpInt = PlayerData.instance.GetInt("MPReserve");

            int additionalCharge = 6; // Gain soul
            if (tmpInt == 0)
                additionalCharge += 5;

            if (PlayerData.instance.GetBool("equippedCharm_20")) // Gain additional soul for Soul Catcher
                if (tmpInt == 0)
                    additionalCharge += 2;
                else
                    additionalCharge += 1;
            if (PlayerData.instance.GetBool("equippedCharm_21")) // Gain additional soul for Soul Eater 
                if (tmpInt == 0)
                    additionalCharge += 4;
                else
                    additionalCharge += 2;

            return additionalCharge;
        }

        // disables hatchling damage from Glowing Womb
        internal static HitInstance HandleGlowingWombDamage(HitInstance hitInstance)
        {
            hitInstance.DamageDealt = 0;
            return hitInstance;
        }

        // disables thorn damage from Thorns of Agony
        internal static HitInstance HandleThornsDamage(HitInstance hitInstance)
        {
            hitInstance.DamageDealt = 0;
            return hitInstance;
        }

        // Disables crystal dash damage
        internal static HitInstance HandleCrystalDashDamage(HitInstance hitInstance)
        {
            hitInstance.DamageDealt = 0;
            return hitInstance;
        }

        // Fixes Parasite Balloon health so that only spells can kill them
        internal static HealthManager HandleParasiteBalloonHit(HealthManager self, HitInstance hitInstance)
        {
            if (self.hp <= 0 && !hitInstance.AttackType.Equals(AttackTypes.Spell)) 
                self.hp = 1; // Parasite Balloons in the Broken Vessel and Lost Kin fights spawn with negative health if previously damaged. 
            return self;
        }
    }
}
