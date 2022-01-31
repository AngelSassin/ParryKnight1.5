using Modding;
using System.Collections.Generic;

namespace ParryKnight
{
    public class EnemyList
    {
        private static string[] parryEnemies = new string[] {
            "Infected Knight",          // Broken Vessel
            "Mawlek Body",              // Brooding Mawlek
            "Mawlek Col",               // Brooding Mawlek, but Colosseum
            "Oro",                      // Brothers Oro & Mato
            "Mato",                     // Brothers Oro & Mato
            "Lancer",                   // God Tamer
            "Great Shield Zombie",      // Great Husk Sentry
            //"Sly Boss",               // Great Nailsage Sly - Not in list because of Phase 2. The check for Phase 2 is in Tools.cs
            "Grimm Boss",               // Grimm
            "Colosseum_Worm",           // Heavy Fool
            "Ruins Sentry Fat",         // Heavy Sentry
            "Ruins Sentry Fat B",       
            "Ruins Sentry FatB",
            "Hive Knight",              // Hive Knight
            "Hollow Knight Boss",       // The Hollow Knight
            "Hornet Boss 2",            // Hornet Sentinel
            "Zombie Miner 1",           // Husk Miner
            "Ruins Sentry 1",           // Husk Sentry
            "Ruins SentryB 1",
            "Zombie Shield",            // Husk Warrior
            "Zombie Shield 1",          
            "Royal Gaurd",              // Kingsmould
            "Lost Kin",                 // Lost Kin
            "Mantis Heavy",             // Mantis Traitor
            "Mantis",                   // Mantis Warrior
            "Moss Knight",              // Moss Knight
            "Moss Knight B",
            "Moss Knight C",
            "Zombie Myla",              // Myla
            "Nightmare Grimm Boss",     // Nightmare King Grimm
            "Sheo Boss",                // Paintmaster Sheo
            "HK Prime",                 // Pure Vessel
            //"Hollow Shade",             // Shade - Not in list because of Voidheart. The check for Voidheart is in Tools.cs
            "Colosseum_Shield_Zombie",  // Shielded Fool
            "Mage Knight",              // Soul Warrior
            "Slash Spider",             // Stalking Devout
            "Colosseum_Miner",          // Sturdy Fool
            "Mantis Traitor Lord",      // Traitor Lord
            "Black Knight 1",           // Watcher Knights
            "Black Knight 2",
            "Black Knight 3",
            "Black Knight 4",
            "Black Knight 5",
            "Black Knight 6",
            "Colosseum_Flying_Sentry",  // Winged Fool
            "Ruins Flying Sentry",      // Winged Sentry
            "Ruins Flying SentryB",
        };

        internal List<string> parryableEnemies = new List<string>(parryEnemies);
    }
}
