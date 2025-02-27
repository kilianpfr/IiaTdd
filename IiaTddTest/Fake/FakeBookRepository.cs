using IiaTdd.cs.Interface;
using IiaTdd.objet;

namespace IiaTddTest.Fake;

public class FakeBookRepository : IBookRepository
{
    List<PostBook> _books = new List<PostBook>
    {
        new PostBook
        {
            Isbn = "1234567890",
            Title = "Le seigneur des anneaux",
            Author = new Author
            {
                Name = "Tolkien",
                FirstName = "J.R.R"
            },
            Editor = "Christian Bourgois",
            Format = FormatEnum.Poche
        },
            
        new PostBook
        {
            Isbn = "9781987654321",
            Title = "Le mystère du passé",
            Author = new Author { FirstName = "Jean", Name = "Renoir" },
            Editor = "Flammarion",
            Format = FormatEnum.Broché
        },
        new PostBook
        {
            Isbn = "9783135792468",
            Title = "Voyage dans le temps",
            Author = new Author { FirstName = "Marie", Name = "Curie" },
            Editor = "Le Seuil",
            Format = FormatEnum.GrandFormat
        }
            
    };
    public PostBook GetBookByISBN(string isbn)
    {
        
        PostBook? book;
        try
        {
             book = _books.FirstOrDefault(x => x.Isbn == isbn);
             if(book == null)
             {
                 throw new Exception("Aucun livre trouvé");
             }
        }catch (Exception e)
        {
            throw new Exception("Aucun livre trouvé");
        }

        return book;
    }
    public bool DeleteBookIsbn(string isbn)
    {
        try
        {
            var book = _books.FirstOrDefault(x => x.Isbn == isbn);
            if (book == null)
            {
                throw new Exception("Aucun livre trouvé");
            }
            _books.Remove(book);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("Aucun livre trouvé");
        }
    }
}
