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

    public objet.PostBookObj GetBookByIsbnForPost(string isbn)
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
            
            
            return new objet.PostBookObj()
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
    
    public void UpdateBookById(int id, PostBookObj bookObj)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE projettdd.livre SET isbn = @isbn, titre = @titre, auteur = @auteur, editeur = @editeur, format = @format WHERE id = @id";
        command.Parameters.AddWithValue("@isbn", bookObj.Isbn);
        command.Parameters.AddWithValue("@titre", bookObj.Title);
        command.Parameters.AddWithValue("@auteur", bookObj.Author.Name + " " + bookObj.Author.FirstName);
        command.Parameters.AddWithValue("@editeur", bookObj.Editor);
        command.Parameters.AddWithValue("@format", bookObj.Format);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
    
    public void AddBook(PostBookObj bookObj)
    {
        //si il manque un champ on essaie l'autocompletion
        if (bookObj.Isbn == null || bookObj.Title == null || bookObj.Author == null || bookObj.Editor == null || bookObj.Format == 0)
        {
            if (bookObj.Isbn != null) bookObj = GetBookByIsbnForPost(bookObj.Isbn);
        }
        
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);
        connection.Open();
        using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO projettdd.livre (isbn, titre, auteur, editeur, format, reserver) VALUES (@isbn, @titre, @auteur, @editeur, @format, false)";
        command.Parameters.AddWithValue("@isbn", bookObj.Isbn);
        command.Parameters.AddWithValue("@titre", bookObj.Title);
        command.Parameters.AddWithValue("@auteur", bookObj.Author.Name + " " + bookObj.Author.FirstName);
        command.Parameters.AddWithValue("@editeur", bookObj.Editor);
        command.Parameters.AddWithValue("@format", bookObj.Format);
        command.ExecuteNonQuery();
    }
    

}