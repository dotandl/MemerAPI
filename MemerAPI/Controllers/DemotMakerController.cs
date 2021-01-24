using System;
using System.IO;
using System.Threading.Tasks;
using MemerAPI.Exceptions;
using MemerAPI.Models;
using MemerAPI.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemerAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class DemotMakerController : ControllerBase
  {
    private DemotMakerWrapper _wrapper = new DemotMakerWrapper();

    private readonly bool _isDev;

    public DemotMakerController(IWebHostEnvironment env) =>
      _isDev = env.EnvironmentName == "Development";

    /*
     * POST /demotmaker
     * Generates demotivator using http://demotmaker.com.pl
     */
    [HttpPost]
    public async Task<IActionResult> Post(
      string title,
      string description,
      [FromForm] IFormFile image)
    {
      try
      {
        byte[] byteArr;
        using (MemoryStream ms = new MemoryStream())
        {
          image.OpenReadStream().CopyTo(ms);
          byteArr = ms.ToArray();
        }

        MemeInfo meme = await _wrapper.GenerateAsync(
          title,
          description,
          byteArr,
          image.FileName);

        Success success = new Success
        {
          Result = meme.Get()
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
