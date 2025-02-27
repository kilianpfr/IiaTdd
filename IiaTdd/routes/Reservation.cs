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
public class ReservationController : ControllerBase
{
    public readonly IConfiguration Configuration;

    public ReservationController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpGet]
    public bool Get()
    {
        return true;
    }
    [HttpPost]
    public IActionResult Post([FromBody] PostReservation reservation)
    {
        if (reservation.Isbn == null || (!CheckIsbnValide.CheckIsbnTen(reservation.Isbn) &&
                                         !CheckIsbnValide.CheckIsbnThirteen(reservation.Isbn))) return BadRequest();
        CheckAuthor.CheckAuthorName(reservation.Author);
        Format.CheckFormatEnum((int) reservation.Format);
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO projettdd.reservation (isbn, titre, auteur, editeur, format, reserver) VALUES (@isbn, @titre, @auteur, @editeur, @format, false)";
        command.Parameters.AddWithValue("@isbn", reservation.Isbn);
        command.Parameters.AddWithValue("@titre", reservation.Title);
        if (reservation.Author != null)
            command.Parameters.AddWithValue("@auteur", reservation.Author.Name + " " + reservation.Author.FirstName);
        command.Parameters.AddWithValue("@editeur", reservation.Editor);
        command.Parameters.AddWithValue("@format", reservation.Format.GetDisplayName());
        command.ExecuteNonQuery();
        return Ok();
    }
}