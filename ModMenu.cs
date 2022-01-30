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
                Name = "Toggle ParryKnight:",
                Description = "Enable or Disable the Mod",
                Values = new string[] { "On", "Off" },
                Saver = (i) => GlobalSaveData.enabled = i == 0,
                Loader = () => GlobalSaveData.enabled ? 0 : 1
            });
            entries.Add(new IMenuMod.MenuEntry
            {
                Name = "Difficulty:",
                Description = "Set the difficulty of ParryKnight",
                Values = new string[] { "Parry Sage", "Parry Master", "Parry Apprentice" },
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
            switch (i)
            {
                case 2: GlobalSaveData.difficulty = CustomGlobalSaveData.DIFFICULTY_SETTING.APPRENTICE; break;
                case 1: GlobalSaveData.difficulty = CustomGlobalSaveData.DIFFICULTY_SETTING.MASTER; break;
                case 0: GlobalSaveData.difficulty = CustomGlobalSaveData.DIFFICULTY_SETTING.SAGE; break;
            }
        }

        public int GetDifficulty()
        {
            switch (GlobalSaveData.difficulty)
            {
                case CustomGlobalSaveData.DIFFICULTY_SETTING.APPRENTICE: return 2;
                case CustomGlobalSaveData.DIFFICULTY_SETTING.MASTER: return 1;
                case CustomGlobalSaveData.DIFFICULTY_SETTING.SAGE: return 0;
            }
            return 0;
        }
    }
}
