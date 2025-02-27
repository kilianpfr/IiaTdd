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
   
    public IActionResult Post([FromBody] PostBook book)
    {
        
        //si tout les champs sont remplis
        if (book.Isbn != null && book.Title != null && book.Author != null && book.Editor != null && book.Format != 0)
        {

            if (book.Isbn == null || (!CheckIsbnValide.CheckIsbnTen(book.Isbn) &&
                                      !CheckIsbnValide.CheckIsbnThirteen(book.Isbn))) return BadRequest();
            CheckAuthor.CheckAuthorName(book.Author);
            Format.CheckFormatEnum((int)book.Format);
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                "INSERT INTO projettdd.livre (isbn, titre, auteur, editeur, format, reserver) VALUES (@isbn, @titre, @auteur, @editeur, @format, false)";
            command.Parameters.AddWithValue("@isbn", book.Isbn);
            command.Parameters.AddWithValue("@titre", book.Title);
            if (book.Author != null)
                command.Parameters.AddWithValue("@auteur", book.Author.Name + " " + book.Author.FirstName);
            command.Parameters.AddWithValue("@editeur", book.Editor);
            command.Parameters.AddWithValue("@format", book.Format.GetDisplayName());
            command.ExecuteNonQuery();
            
        }
        else
        {
            var empty = new BookWIthNullData(_repository);
            var Book = empty.AutoComplete(book.Isbn);
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
    
    
}