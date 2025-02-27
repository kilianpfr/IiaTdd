using IiaTdd.cs.Interface;
using IiaTdd.cs.Isbn;
using IiaTdd.objet;

namespace IiaTdd.cs;

public class BookWIthNullData
{
    private readonly IBookRepository _repository;

    public BookWIthNullData(IBookRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public PostBook AutoComplete(string? isbn)
    {
        if (isbn is null)
            throw new ArgumentNullException(nameof(isbn));

   
        CheckIsbn.TenOrThirteen(isbn);
        if (isbn.Length == 10)
            CheckIsbnValide.CheckIsbnTen(isbn);
        else
            CheckIsbnValide.CheckIsbnThirteen(isbn);

       
        return _repository.GetBookByISBN(isbn);
    }
}