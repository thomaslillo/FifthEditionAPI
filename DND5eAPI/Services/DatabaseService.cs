using DND5eAPI.Models;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using DND5eAPI.Services;

namespace DND5eAPI.Data
{
    public class DatabaseService : IDatabaseService
    {

        private readonly IConfiguration _config;
        private readonly ILogger<DatabaseService> _logger;

        private string? _DBPath;

        public DatabaseService(ILogger<DatabaseService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            // get the DB location from settings and store it in the object
            _DBPath = _config["DB"];
        }

        public string searchDB(string searchterm, string property)
        {

            if (!string.IsNullOrWhiteSpace(_DBPath))
            {

                JsonElement root = getJsonRoot(_DBPath);
                var enumRoot = root.EnumerateArray();

                // start off with the "return" being null
                string? selectedItem = null;

                // find the elemenet that matches the index and save it
                while (enumRoot.MoveNext())
                {
                    var currentItem = enumRoot.Current;

                    string curIndex = currentItem.GetProperty(property).ToString();

                    if (curIndex == searchterm)
                    {
                        selectedItem = currentItem.ToString();
                    }
                }

                // if found return it
                if (!string.IsNullOrEmpty(selectedItem))
                {
                    return selectedItem;
                }
                else
                {
                    return "ERROR: Spell Not Found";
                }

            }
            else
            {
                // write the error to the logs
                //_logger.Log();
                return "Error Connecting to Database";
            }            
        }

        public bool writeToDB(string data)
        {

            if (!string.IsNullOrWhiteSpace(_DBPath))
            {

                JsonElement root = getJsonRoot(_DBPath);                

                string _data = File.ReadAllText(_DBPath);
                
                Spell weatherForecast = JsonSerializer.Deserialize<Spell>(jsonString)!;

                string jsonString = JsonSerializer.Serialize(data);

                File.WriteAllText(_DBPath, jsonString);                              

            }

            return true;
        }

        private JsonElement getJsonRoot(string path)
        {
            string? _data;
            
            // read the file
            _data = File.ReadAllText(path);

            // parse into JSON and get the root element
            using JsonDocument doc = JsonDocument.Parse(_data);
            JsonElement root = doc.RootElement;

            return root;
        }
    }
}