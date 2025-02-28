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

       
        return _repository.GetBookByIsbnForPost(isbn);
    }
    public void AddBook(PostBookObj bookObj)
    {
        if(bookObj.Isbn == null)
            throw new Exception("Isbn invalide");
        
        
        if (bookObj.Title == null || bookObj.Author == null || bookObj.Editor == null || bookObj.Format == 0)
        {
            
            
            CheckIsbn.TenOrThirteen(bookObj.Isbn);
            if (bookObj.Isbn.Length == 10)
                CheckIsbnValide.CheckIsbnTen(bookObj.Isbn);
            else
                CheckIsbnValide.CheckIsbnThirteen(bookObj.Isbn);
            
            bookObj = AutoComplete(bookObj.Isbn);
        }

        
        CheckAuthor.CheckAuthorName(bookObj.Author);
        
        Format.CheckFormatEnum((int)bookObj.Format);
        _repository.AddBook(bookObj);
        
        
    }

  
}