
using System;
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

    private readonly ILogger<WeatherForecastController> _logger;

    // dependency injection in the controller
    public SpellController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }



}
