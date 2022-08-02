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

        public string DamageType { get; set; }

        public Hashtable DamageSlotLvl { get; set; }

        public Hashtable DamageCharLvl { get; set; }

        public string School { get; set; }

        public Array Classes { get; set; }

        public Hashtable HealSlotLvl { get; set; }

        public Hashtable DCType { get; set; }

        string DCSuccess { get; set; }

        string DCDescription { get; set; }

        string AreaOfEffectType { get; set; }

        double AreaOfEffectSize { get; set; }

    }
}