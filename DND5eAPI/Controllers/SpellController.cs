
using System;
using DND5eAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

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

    // dependency injection in the controller
    public SpellController(ILogger<SpellController> logger)
    {
        _logger = logger;
    }

    // temp data
    public static List<Spell> _spells = new List<Spell>
    {
        new Spell {Id = "tom", Name = "tname", Desc = "description"},
        new Spell {Id = "tom2", Name = "tname2", Desc = "description2"},
        new Spell {Id = "tom3", Name = "tname3", Desc = "description3"}
    };

    // return a single spell
    [HttpGet]
    public async Task<ActionResult<Spell>> GetSpell(string id)
    {
        Spell? return_spell = _spells.Find(s => s.Id == id);

        if (return_spell == null)
        {
            return BadRequest("Hero Not Found");
        }
        else
        {
            return Ok(return_spell);
        }

    }

    // return a list of multiple spells
    [HttpGet]
    [Route("MultipleSpells")]
    public async Task<ActionResult<List<Spell>>> GetSpells()
    {

        // return with status code 200
        return Ok(_spells);

    }

    // add a spell to the list
    [HttpPost]
    public async Task<ActionResult<Spell>> AddHomebrewSpell(Spell newSpell)
    {
        // check if the new spell is good

        // add the spell to the list
        _spells.Add(newSpell);
        return Ok(_spells);
    }


}
