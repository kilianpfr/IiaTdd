using IiaTdd.cs.Interface;
using IiaTdd.cs.Isbn;
using IiaTdd.objet;

namespace IiaTddTest.Fake;

public class FakeBookRepository : IBookRepository
{
    List<GetBookObj> _books = new List<GetBookObj>
    {
        new GetBookObj()
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
            
        new GetBookObj
        {
            Id = 2,
            Isbn = "9781987654321",
            Title = "Le mystère du passé",
            Author = new Author { FirstName = "Jean", Name = "Renoir" },
            Editor = "Flammarion",
            Format = FormatEnum.Broché
        },
        new GetBookObj
        {
            Id=3,
            Isbn = "9783135792468",
            Title = "Voyage dans le temps",
            Author = new Author { FirstName = "Marie", Name = "Curie" },
            Editor = "Le Seuil",
            Format = FormatEnum.GrandFormat
        }
            
    };
    public PostBookObj GetBookByIsbnForPost(string isbn)
    {
        
        PostBookObj? book;
        try
        {
            var getbook = _books.FirstOrDefault(x => x.Isbn == isbn);
             book = new PostBookObj()
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
    public void UpdateBookById(int id, PostBookObj bookObj)
    {
        try
        {
            var bookToUpdate = _books.FirstOrDefault(x => x.Id == id);
            if (bookToUpdate == null)
            {
                throw new Exception("Aucun livre trouvé");
            }
            bookToUpdate.Isbn = bookObj.Isbn;
            bookToUpdate.Title = bookObj.Title;
            bookToUpdate.Author = bookObj.Author;
            bookToUpdate.Editor = bookObj.Editor;
            bookToUpdate.Format = bookObj.Format;
        }
        catch (Exception e)
        {
            throw new Exception("Aucun livre trouvé");
        }
    }

    public void AddBook(PostBookObj bookObj)
    {
            _books.Add(new GetBookObj()
            {
                Id = _books.Count + 1,
                Isbn = bookObj.Isbn,
                Title = bookObj.Title,
                Author = bookObj.Author,
                Editor = bookObj.Editor,
                Format = bookObj.Format
            });
    }

    public List<GetBookObj> GetBookByAuthor(Author author)
    {
        
            var books = _books.Where(x => x.Author == author).ToList();
            if (books.Count == 0)
            {
                throw new Exception("Aucun livre trouvé");
            }
            return books;
        
       
    }
}
