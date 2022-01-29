using System;
using System.Collections.Generic;
using Modding;
using UnityEngine;

namespace ParryKnight
{
    public partial class ParryKnight : IMenuMod
    {
        public bool ToggleButtonInsideMenu => false;

        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            List<IMenuMod.MenuEntry> entries = new List<IMenuMod.MenuEntry>();

            entries.Add(new IMenuMod.MenuEntry
            {
                Name = "Difficulty:",
                Description = "Set the difficulty of ParryKnight, or turn it off entirely",
                Values = new string[] { "Baby", "Parry Apprentice", "Parry Master", "Parry Sage" },
                Saver = SetDifficulty,
                Loader = GetDifficulty
            });
            entries.Add(new IMenuMod.MenuEntry
            {
                Name = "Pantheon Boost:",
                Description = "Modifies damage taken by bosses in the Pantheons",
                Values = new string[] { "None", "1.5x", "2.0x" },
                Saver = (i) => GlobalSaveData.pantheonBoost = (i + 2) / 2.0,
                Loader = () => (int) (GlobalSaveData.pantheonBoost * 2.0) - 2
            });

            return entries;
        }

        public void SetDifficulty(int i)
        {
            if (i == 0)
            {
                GlobalSaveData.enabled = false;
                return;
            }
            GlobalSaveData.enabled = true;

            switch (i)
            {
                case 1: GlobalSaveData.difficulty = CustomGlobalSaveData.DIFFICULTY_SETTING.APPRENTICE; break;
                case 2: GlobalSaveData.difficulty = CustomGlobalSaveData.DIFFICULTY_SETTING.MASTER; break;
                case 3: GlobalSaveData.difficulty = CustomGlobalSaveData.DIFFICULTY_SETTING.SAGE; break;
            }

        }

        public int GetDifficulty()
        {
            if (!GlobalSaveData.enabled)
                return 0;

            switch (GlobalSaveData.difficulty)
            {
                case CustomGlobalSaveData.DIFFICULTY_SETTING.APPRENTICE: return 1;
                case CustomGlobalSaveData.DIFFICULTY_SETTING.MASTER: return 2;
                case CustomGlobalSaveData.DIFFICULTY_SETTING.SAGE: return 3;
            }
            return 1;
        }
    }
}
