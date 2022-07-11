using System.ComponentModel.DataAnnotations;

namespace DND5eAPI.Data.Models
{
    public class Spell
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
    }
}
