using DND5eAPI.Models;

namespace DND5eAPI.Services
{
    public interface IDatabaseService
    {

        public string searchDB(string searchterm, string property);

        public bool writeToDB(string data);

    }
}
