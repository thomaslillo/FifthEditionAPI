using DND5eAPI.Models;

namespace DND5eAPI.Services
{
    public interface IDatabaseService
    {
        List<Spell> getSpells();

        Spell getSpellByName(string name);

        List<Spell> getSpellsWith(string param, string filter);

    }
}
