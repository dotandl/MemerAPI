using System;
using System.Threading.Tasks;
using MemerAPI.Exceptions;
using MemerAPI.Models;
using MemerAPI.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MemerAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class KwejkController : ControllerBase
  {
    private readonly bool _isDev;

    public KwejkController(IWebHostEnvironment env) =>
      _isDev = env.EnvironmentName == "Development";

    /*
     * GET /kwejk
     * Gets a random image from https://kwejk.pl
     */
    public async Task<IActionResult> Get()
    {
      try
      {
        Success success = new Success
        {
          Result = await KwejkWrapper.Random()
        };

        return Ok(success.Get());
      }
      catch (NotFoundException ex)
      {
        Error err = new Error
        {
          Code = 1,
          Message = "A given image could not be found",
          Exception = ex
        };

        return NotFound(err.Get(_isDev));
      }
      catch (ServiceOrConnectionException ex)
      {
        Error err = new Error
        {
          Code = 2,
          Message = "An error occurred while connecting to the external service",
          Exception = ex
        };

        return StatusCode(502, err.Get(_isDev));
      }
      catch (Exception ex)
      {
        Error err = new Error
        {
          Code = 3,
          Message = "An unexpected error occurred",
          Exception = ex
        };

        return StatusCode(500, err.Get(_isDev));
      }
    }
  }
}