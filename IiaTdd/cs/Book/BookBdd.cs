using IiaTdd.cs.Interface;
using IiaTdd.objet;
using MySql.Data.MySqlClient;

namespace IiaTdd.cs.Book;

public class BookBdd : IBookRepository
{
    private readonly IConfiguration _configuration;

    public BookBdd(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public PostBook GetBookByIsbn(string isbn)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT id, isbn, titre, auteur, editeur, format, reserver FROM projettdd.livre WHERE isbn = @isbn";
        command.Parameters.AddWithValue("@isbn", isbn);
        
        using MySqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            
            //on split le nom et le prenom par l'espace
            string[] author = reader.GetString("auteur").Split(' ');
            
            
            return new PostBook()
            {
             
                Isbn = reader.GetString("isbn"),
                Title = reader.GetString("titre"),
                Author = new objet.Author()
                {
                    Name = author[0],
                    FirstName = author[1]
                },
                Editor = reader.GetString("editeur"),
                Format = (FormatEnum)reader.GetInt32("format")
            };
        }
        else
        {
            throw new Exception("Aucun livre trouvé");
        }
    }

    public bool DeleteBookById(int  id)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM projettdd.livre WHERE isbn = @isbn";
        command.Parameters.AddWithValue("@isbn", id);
        command.ExecuteNonQuery();
        return true;
    }
    
    public void UpdateBookById(int id, PostBook book)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE projettdd.livre SET isbn = @isbn, titre = @titre, auteur = @auteur, editeur = @editeur, format = @format WHERE id = @id";
        command.Parameters.AddWithValue("@isbn", book.Isbn);
        command.Parameters.AddWithValue("@titre", book.Title);
        command.Parameters.AddWithValue("@auteur", book.Author.Name + " " + book.Author.FirstName);
        command.Parameters.AddWithValue("@editeur", book.Editor);
        command.Parameters.AddWithValue("@format", book.Format);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
       
        
    }
}