using IiaTdd.cs.Author;
using IiaTdd.cs.format;
using IiaTdd.cs.Isbn;
using IiaTdd.objet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using MySql.Data.MySqlClient;

namespace IiaTdd.routes;
[ApiController]
[Route("[controller]")]
public class BookingController(IConfiguration configuration) : ControllerBase
{
    public readonly IConfiguration Configuration = configuration;

    [HttpGet]
    public bool Get()
    {
        return true;
    }
    [HttpPost]
    public IActionResult Post([FromBody] PostBookObj bookObj)
    {
        return Ok();
    }
}