using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace DND5eAPI.Models
{
    public class Spell
    {
        [Key]
        public string Index { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }
        
        public string? HigherLevelDesc { get; set; }

        public string? Range { get; set; }

        public List<string> Components { get; set; }

        public string Materials { get; set; }

        public string Duration { get; set; }

        public bool IsRitual { get; set; }

        public bool IsConcentration { get; set; }

        public string CastingTime { get; set; }

        public int Level { get; set; }

        public string AttackType { get; set; }

        public Damage SpellDamage { get; set; }

        public string School { get; set; }

        public List<string> Classes { get; set; }

        Hashtable HealSlotLvl { get; set; }

        DC SpellDC { get; set; }
        AreaOfEffect SpellAreaOfEffect { get; set; }
    }
}