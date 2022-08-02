using System.Collections;

namespace DND5eAPI.Models
{
    public class Damage
    {
        string DamageType { get; set; }

        Hashtable DamageSlotLvl {get; set;}

        Hashtable DamageCharLvl { get; set; }

    }
}