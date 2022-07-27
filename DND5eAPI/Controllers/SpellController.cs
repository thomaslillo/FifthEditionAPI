
using System;
using DND5eAPI.Data.Models;
using DND5eAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 

namespace DND5eAPI.Controllers;

// indicated the type and route
// in brackets is the controller token - a convention
// token controller and will take the name of the class as the controller
// everything that comes before Controller in the name
[ApiController]
[Route("[controller]")]
public class SpellController : ControllerBase
{

    private readonly ILogger<SpellController> _logger;
    private readonly IConfiguration _config;

    // dependency injection in the controller
    public SpellController(ILogger<SpellController> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    // return a single spell
    [HttpGet]
    public async Task<ActionResult<string>> GetSpell(string id)
    {
        // get the path from app settings
        string spellsPath = _config["TempDBFile"];

        // use the DB reader object
        DBReader _dbReader = new DBReader();

        string return_spell = _dbReader.readSpell(spellsPath, id);

        // will use the Spell model when DB hooked up
        // Spell? return_spell = _spells.Find(s => s.Id == id);

        if (!string.IsNullOrWhiteSpace(return_spell))                       
        {
            return Ok(return_spell);
        }
        else
        {
            return BadRequest("Spell Not Found");
        }
    }

}
