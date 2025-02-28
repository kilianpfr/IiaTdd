using IiaTdd.cs.Author;
using IiaTdd.cs.format;
using IiaTdd.cs.Interface;
using IiaTdd.cs.Isbn;
using IiaTdd.objet;

namespace IiaTdd.cs.Book;

public class PostBook
{
    private readonly IBookRepository _repository;

    public PostBook(IBookRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public PostBookObj AutoComplete(string? isbn)
    {
        if (isbn is null)
            throw new ArgumentNullException(nameof(isbn));

   
        CheckIsbn.TenOrThirteen(isbn);
        if (isbn.Length == 10)
            CheckIsbnValide.CheckIsbnTen(isbn);
        else
            CheckIsbnValide.CheckIsbnThirteen(isbn);

       
        return _repository.GetBookByIsbn(isbn);
    }
    public void AddBook(PostBookObj bookObj)
    {
        //aucun des champs ne doit être null
        if (bookObj.Isbn == null || bookObj.Title == null || bookObj.Author == null || bookObj.Editor == null || bookObj.Format == 0)
        {
            throw new ArgumentNullException();
        }
        //si l'isbn n'est pas valide
        if (bookObj.Isbn == null || (!CheckIsbnValide.CheckIsbnTen(bookObj.Isbn) &&
                                  !CheckIsbnValide.CheckIsbnThirteen(bookObj.Isbn))) throw new ArgumentNullException("Isbn invalide");
        //si l'auteur n'est pas valide
        CheckAuthor.CheckAuthorName(bookObj.Author);
        //si le format n'est pas valide
        Format.CheckFormatEnum((int)bookObj.Format);
        _repository.AddBook(bookObj);
        
        
    }
}