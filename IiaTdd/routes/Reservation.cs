using IiaTdd.cs.Author;
using IiaTdd.cs.Isbn;
using IiaTdd.objet;
using Microsoft.AspNetCore.Mvc;

namespace IiaTdd.routes;
[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    
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
        
        
        return Ok();
    }
}