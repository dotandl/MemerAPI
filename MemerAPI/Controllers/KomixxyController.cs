using System;
using MemerAPI.Models;
using MemerAPI.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MemerAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class KomixxyController : ControllerBase
  {
    private bool _isDev;

    public KomixxyController(IWebHostEnvironment env) =>
      _isDev = env.EnvironmentName == "Development";

    /*
     * GET /komixxy
     * Get a random image from https://komixxy.pl
     */
    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        Success success = new Success
        {
          Result = KomixxyWrapper.Random()
        };

        return Ok(success.Get());
      }
      catch (NotImplementedException ex)
      {
        Error err = new Error
        {
          Code = 1, // temporary
          Message = "Random image getting has not been implemented for Komixxy yet",
          Exception = ex
        };

        return StatusCode(500, err.Get(_isDev));
      }
    }
  }
}
