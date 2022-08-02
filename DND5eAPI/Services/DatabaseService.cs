using DND5eAPI.Data.Models;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DND5eAPI.Data
{
    public class DatabaseService
    {
        // until DB is hooked up read the spells this way
        public string readSpell(string filePath, string index)
        {
            string? _data;

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                // read the file
                _data = File.ReadAllText(filePath);
            }
            else
            {
                return "ERROR: reading or parsing file path";
            }

            // parse into JSON and get the root element
            using JsonDocument doc = JsonDocument.Parse(_data);
            JsonElement root = doc.RootElement;

            string? spell = null;

            // find the elemenet that matches the index and save it
            var spells = root.EnumerateArray();

            while (spells.MoveNext())
            {
                var currentSpell = spells.Current;

                string curIndex = currentSpell.GetProperty("index").ToString();

                if (curIndex == index)
                {
                    spell = currentSpell.ToString();
                }
            }

            // if found return it
            if (!string.IsNullOrEmpty(spell))
            {
                return spell;
            }
            else
            {
                return "ERROR: Spell Not Found";
            }
        }
    }
}
