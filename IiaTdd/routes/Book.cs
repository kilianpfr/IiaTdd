using IiaTdd.cs;
using IiaTdd.cs.Author;
using IiaTdd.cs.Book;
using IiaTdd.cs.format;
using IiaTdd.cs.Interface;
using IiaTdd.cs.Isbn;
using IiaTdd.objet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using MySql.Data.MySqlClient;


namespace IiaTdd.routes;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    public readonly IConfiguration Configuration;
    
    public readonly IBookRepository _repository;

    public BookController(IConfiguration configuration, IBookRepository repository)
    {
        Configuration = configuration;
        _repository = repository;
    }
   
    public IActionResult Post([FromBody] PostBookObj bookObj)
    {
        
        //si tout les champs sont remplis
        if (bookObj.Isbn != null && bookObj.Title != null && bookObj.Author != null && bookObj.Editor != null && bookObj.Format != 0)
        {

            if (bookObj.Isbn == null || (!CheckIsbnValide.CheckIsbnTen(bookObj.Isbn) &&
                                      !CheckIsbnValide.CheckIsbnThirteen(bookObj.Isbn))) return BadRequest();
            CheckAuthor.CheckAuthorName(bookObj.Author);
            Format.CheckFormatEnum((int)bookObj.Format);
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO projettdd.livre (isbn, titre, auteur, editeur, format, reserver) VALUES (@isbn, @titre, @auteur, @editeur, @format, false)";
            command.Parameters.AddWithValue("@isbn", bookObj.Isbn);
            command.Parameters.AddWithValue("@titre", bookObj.Title);
            if (bookObj.Author != null)
                command.Parameters.AddWithValue("@auteur", bookObj.Author.Name + " " + bookObj.Author.FirstName);
            command.Parameters.AddWithValue("@editeur", bookObj.Editor);
            command.Parameters.AddWithValue("@format", bookObj.Format.GetDisplayName());
            command.ExecuteNonQuery();
            
        }
        else
        {
            var empty = new PostBook(_repository);
            var Book = empty.AutoComplete(bookObj.Isbn);
            if (Book == null) return BadRequest();
            using var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO projettdd.livre (isbn, titre, auteur, editeur, format, reserver) VALUES (@isbn, @titre, @auteur, @editeur, @format, false)";
            command.Parameters.AddWithValue("@isbn", Book.Isbn);
            command.Parameters.AddWithValue("@titre", Book.Title);
            if (Book.Author != null)
                command.Parameters.AddWithValue("@auteur", Book.Author.Name + " " + Book.Author.FirstName);
            command.Parameters.AddWithValue("@editeur", Book.Editor);
            command.Parameters.AddWithValue("@format", Book.Format.GetDisplayName());
            command.ExecuteNonQuery();
            
        }
        return Ok();
        
    }
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        if (id == 0) return BadRequest();
        var delete = new DeleteBook(_repository);
        var result = delete.DeleteBookIdRep(id);
        if (result) return Ok();
        return BadRequest();
    }
    [HttpPut]
    public IActionResult Put(int id, [FromBody] PostBookObj bookObj)
    {
        if (id == 0) return BadRequest();
        if (bookObj.Isbn == null || (!CheckIsbnValide.CheckIsbnTen(bookObj.Isbn) &&
                                      !CheckIsbnValide.CheckIsbnThirteen(bookObj.Isbn))) return BadRequest();
        CheckAuthor.CheckAuthorName(bookObj.Author);
        Format.CheckFormatEnum((int)bookObj.Format);
        var update = new UpdateBook(_repository);
        update.UpdateBookIdRep(id, bookObj);
        return Ok();
    }
    
    
    
}