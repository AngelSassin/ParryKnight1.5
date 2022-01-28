using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParryKnight
{
    class TextReplacements
    {
        private static Dictionary<string, string> replacements = new Dictionary<string, string>() 
        {
            // GENERAL DESCRIPTIONS
            { "DIRTMOUTH_MAIN", "Parry Knight" },
            { "DIRTMOUTH_SUB", "Damage Enemies by Parrying" },
            { "FALSE_KNIGHT_DREAM_MAIN", "Failed Chump" },
            { "FALSE_KNIGHT_DREAM_SUB", "Nail Arts are your friend" },

            // NAIL DESCRIPTIONS
            { "INV_DESC_NAIL1", "A traditional weapon of Hallownest. Its blade is blunt with age and wear.<br><br>Damage is only dealt by parrying. Nail strikes will gather soul." },
            { "INV_DESC_NAIL2", "A traditional weapon of Hallownest, restored to lethal form.<br><br>Damage is only dealt by parrying. Nail strikes will gather soul." },
            { "INV_DESC_NAIL3", "A cleft weapon of Hallownest. The blade is exquisitly balanced.<br><br>Damage is only dealt by parrying. Nail strikes will gather soul." },
            { "INV_DESC_NAIL4", "A powerful weapon of Hallownest, refined beyond all others.<br><br>Damage is only dealt by parrying. Nail strikes will gather soul." },
            { "INV_DESC_NAIL5", "The ultimate weapon of Hallownest. Crafted to perfection, this ancient nail reveals its true form.<br><br>Damage is only dealt by parrying. Nail strikes will gather soul." },

            // OBTAIN SPELL DESCRIPTIONS
            { "GET_FIREBALL_2", "Spells will deplete SOUL. Replenish SOUL by striking enemies.<br>Only non-parryable enemies will be damaged by this spell." },
            { "GET_FIREBALL2_2", "Spells will deplete SOUL. Replenish SOUL by striking enemies.<br>Only non-parryable enemies will be damaged by this spell." },
            { "GET_QUAKE_2", "Spells will deplete SOUL. Replenish SOUL by striking enemies.<br>Only non-parryable enemies will be damaged by this spell." },
            { "GET_QUAKE2_2", "Spells will deplete SOUL. Replenish SOUL by striking enemies.<br>Only non-parryable enemies will be damaged by this spell." },
            { "GET_SCREAM_2", "Spells will deplete SOUL. Replenish SOUL by striking enemies.<br>Only non-parryable enemies will be damaged by this spell." },
            { "GET_SCREAM2_2", "Spells will deplete SOUL. Replenish SOUL by striking enemies.<br>Only non-parryable enemies will be damaged by this spell." },

            // OBTAIN NAIL ART DESCRIPTIONS
            { "GET_CYCLONE_2", "Release while holding UP or DOWN to perform the Cyclone Slash.<br>Nail Arts deal no damage, but they will gather a lot of soul." },
            { "GET_DSLASH_2", "Release the button while dashing to perform the Dash Slash.<br>Nail Arts deal no damage, but they will gather a lot of soul." },
            { "GET_GSLASH_2", "Release the button without holding UP or DOWN to perform the Great Slash.<br>Nail Arts deal no damage, but they will gather a lot of soul." },

            // INVENTORY SPELL DESCRIPTIONS
            { "INV_DESC_SPELL_FIREBALL1", "Conjure a spirit that will fly forward and burn foes in its path.<br><br>The spirit requires SOUL to be conjured. Strike enemies to gather SOUL.<br><br>Only non-parryable enemies will be damaged by this spell." },
            { "INV_DESC_SPELL_FIREBALL2", "Conjure a shadow that will fly forward and burn foes in its path.<br><br>The shadow requires SOUL to be conjured. Strike enemies to gather SOUL.<br><br>Only non-parryable enemies will be damaged by this spell." },
            { "INV_DESC_SPELL_QUAKE1", "Strike the ground with a concentrated force of SOUL. This force can destroy foes or break through fragile structures.<br><br>The force requires SOUL to be conjured. Strike enemies to gather SOUL.<br><br>Only non-parryable enemies will be damaged by this spell." },
            { "INV_DESC_SPELL_QUAKE2", "Strike the ground with a concentrated force of SOUL and Shadow. This force can destroy foes or break through fragile structures.<br><br>The force requires SOUL to be conjured. Strike enemies to gather SOUL.<br><br>Only non-parryable enemies will be damaged by this spell." },
            { "INV_DESC_SPELL_SCREAM1", "Blast foes with screaming SOUL.<br><br>The Wraiths requires SOUL to be conjured. Strike enemies to gather SOUL.<br><br>Only non-parryable enemies will be damaged by this spell." },
            { "INV_DESC_SPELL_SCREAM2", "Blast foes with screaming SOUL and Shadow.<br><br>The Wraiths requires SOUL to be conjured. Strike enemies to gather SOUL.<br><br>Only non-parryable enemies will be damaged by this spell." },

            // INVENTORY NAIL ARTS DESCRIPTIONS
            { "INV_DESC_ART_CYCLONE", "The signature Nail Art of Nailmaster Mato. A spinning attack that rapidly strikes foes on all sides.<br><br>Nail Arts deal no damage, but they will gather more soul than a normal nail strike." },
            { "INV_DESC_ART_UPPER", "The signature Nail Art of Nailmaster Oro. Strike ahead quickly after dashing forward.<br><br>Nail Arts deal no damage, but they will gather more soul than a normal nail strike." },
            { "INV_DESC_ART_DASH", "The signature Nail Art of Nailmaster Sheo. Unleashes a huge slash directly in front of you which deals extra damage to foes.<br><br>Nail Arts deal no damage, but they will gather more soul than a normal nail strike." },

            // CHARM DESCRIPTIONS
            { "CHARM_DESC_35", "Contains the gratitude of grubs who will move to the next stage of their lives. Imbues weapons with a holy strength.<br><br>When the bearer is at full health, they will fire beams of white-hot energy from their nail.<br><br>This beam deals no damage." },
            { "CHARM_DESC_6", "Embodies the fury and heroism that comes upon those who are about to die.<br><br>When close to death, parrying will deal more damage." },
            { "CHARM_DESC_25", "Strengthens the bearer, increasing the damage they deal to enemies.<br><br>This charm is fragile, and will break if its bearer is killed.<br><br>When equipped, parrying will deal more damage." },
            { "CHARM_DESC_25_G", "Strengthens the bearer, increasing the damage they deal to enemies.<br><br>This charm is unbreakable.<br><br>When equipped, parrying will deal more damage." },
            { "CHARM_DESC_12", "Senses the pain of its bearer and lashes out at the world around them.<br><br>When taking damage, sprout thorny vines.<br><br>Vines deal no damage." },
            { "CHARM_DESC_11", "Living charm born in the gut of a Flukemarm.<br><br>Transforms the Vengeful Spirit spell into a horde of volatile baby flukes.<br><br>Only non-parryable enemies will be damaged by flukes." },
            { "CHARM_DESC_10", "Unique charm bestowed by the King of Hallownest to his most loyal knight. Scratched and dirty, but still cared for.<br><br>Causes the bearer to emit a heroic odour.<br><br>The odor deals no damage." },
            { "CHARM_DESC_22", "Drains the SOUL of its bearer and uses it to birth hatchlings.<br><br>The hatchlings have no desire to eat or live, and will sacrifice themselves to protect their parent.<br><br>Hatchlings deal no damage." },
            { "CHARM_DESC_38", "Defensive charm once wielded by a tribe that could shape dreams.<br><br>Conjures a shield that follows the bearer and attempts to protect them.<br><br>The shield deals no damage." },
            { "CHARM_DESC_39", "Silken charm containing a song of farewell, left by the Weavers who departed Hallownest for their old home.<br><br>Summons weaverlings to give the lonely bearer some companionship and protection.<br><br>Weaverlings deal no damage." },
            { "CHARM_DESC_26", "Contains the passion, skill and regrets of a Nailmaster.<br><br>Increases the bearer's mastery of Nail Arts, allowing them to focus their power faster and unleash arts sooner.<br><br>Nail Arts will gather soul more effectively." },
            { "CHARM_DESC_16", "Contains a forbidden spell that transforms shadows into deadly weapons.<br><br>When using Shadow Dash, the bearer's body will sharpen.<br><br>Shadow Dashing deals no damage." },
            { "CHARM_DESC_17", "Composed of living fungal matter.<br><br>Spore Shroom deals no damage." },
            { "CHARM_DESC_40", "Worn by those who take part in the Grimm Troupe's Ritual.<br><br>The bearer must seek the Grimmkin and collect their flames. Uncollected flames will appear on the bearer's map.<br><br>Grimmchild will never deal damage." },
            { "CHARM_DESC_40_F", "Symbol of a completed ritual. Contains a living, scarlet flame.<br><br>Grimmchild deals no damage." },
        };

        internal static string Get(string key)
        {
            if (!replacements.Keys.Contains(key))
                return null;
            return replacements[key];
        }
    }
}
