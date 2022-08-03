
using System;
using DND5eAPI.Data;
using DND5eAPI.Models;
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
    private readonly AppDataContext _database;

    // dependency injection in the controller
    public SpellController(ILogger<SpellController> logger, IConfiguration config, AppDataContext datacontext)
    {
        _logger = logger;
        _config = config;
        _database = datacontext;
    }


    [HttpPost]
    public async Task<ActionResult<Spell>> PostSpell(Spell newSpell)
    {
        // add the new spell to the database
        _database.Spells.Add(newSpell);

        // save the changes to the database
        await _database.SaveChangesAsync();

        return CreatedAtAction("GetSpell", new { index = newSpell.Index }, newSpell);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Spell>> GetSpell(string id)
    {
        Spell spell = await _database.Spells.FindAsync(id);

        if (spell == null)
        {
            return NotFound();
        }

        return spell;

    }

    

    // return a single spell
    //[HttpGet]
    //public async Task<ActionResult<string>> GetSpellJSON(string id)
    //{
    //    // get the path from app settings
    //    string spellsPath = _config["TempDBFile"];

    //    // use the DB reader object
    //    DatabaseService _dbReader = new DatabaseService();

    //    string return_spell = _dbReader.readSpell(spellsPath, id);

    //    // will use the Spell model when DB hooked up
    //    // Spell? return_spell = _spells.Find(s => s.Id == id);

    //    if (!string.IsNullOrWhiteSpace(return_spell))                       
    //    {
    //        return Ok(return_spell);
    //    }
    //    else
    //    {
    //        return BadRequest("Spell Not Found");
    //    }
    //}

}
