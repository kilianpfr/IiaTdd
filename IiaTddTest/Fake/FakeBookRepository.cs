using IiaTdd.cs.Interface;
using IiaTdd.cs.Isbn;
using IiaTdd.objet;

namespace IiaTddTest.Fake;

public class FakeBookRepository : IBookRepository
{
    List<GetBook> _books = new List<GetBook>
    {
        new GetBook()
        {
            Id = 1,
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
            
        new GetBook
        {
            Id = 2,
            Isbn = "9781987654321",
            Title = "Le mystère du passé",
            Author = new Author { FirstName = "Jean", Name = "Renoir" },
            Editor = "Flammarion",
            Format = FormatEnum.Broché
        },
        new GetBook
        {
            Id=3,
            Isbn = "9783135792468",
            Title = "Voyage dans le temps",
            Author = new Author { FirstName = "Marie", Name = "Curie" },
            Editor = "Le Seuil",
            Format = FormatEnum.GrandFormat
        }
            
    };
    public PostBook GetBookByIsbn(string isbn)
    {
        
        PostBook? book;
        try
        {
            var getbook = _books.FirstOrDefault(x => x.Isbn == isbn);
             book = new PostBook()
             {
                 Isbn = getbook?.Isbn,
                 Title = getbook?.Title,
                 Author = getbook?.Author,
                 Editor = getbook?.Editor,
                 Format = getbook.Format
             };
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
    public bool DeleteBookById(int id)
    {
        try
        {
            var book = _books.FirstOrDefault(x => x.Id == id);
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
    public void UpdateBookById(int id, PostBook book)
    {
        try
        {
            
          
            
            var bookToUpdate = _books.FirstOrDefault(x => x.Id == id);
            if (bookToUpdate == null)
            {
                throw new Exception("Aucun livre trouvé");
            }
            bookToUpdate.Isbn = book.Isbn;
            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Editor = book.Editor;
            bookToUpdate.Format = book.Format;
        }
        catch (Exception e)
        {
            throw new Exception("Aucun livre trouvé");
        }
    }
}
