using System;
using System.Threading.Tasks;
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
    public async Task<IActionResult> Get()
    {
      try
      {
        Success success = new Success
        {
          Result = await KomixxyWrapper.Random()
        };

        return Ok(success.Get());
      }
      catch (Exception ex)
      {
        Error err = new Error
        {
          Code = 1, // temporary
          Message = "An unexpected error occurred",
          Exception = ex
        };

        return StatusCode(500, err.Get(_isDev));
      }
    }
  }
}
