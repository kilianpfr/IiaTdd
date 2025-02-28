using IiaTdd.cs.Interface;
using IiaTdd.objet;

namespace IiaTdd.cs.Book;

public class GetBook
{
    
    private readonly IBookRepository _repository;
    
    public GetBook(IBookRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public List<GetBookObj> GetBookByAuthor(objet.Author author)
    {
        if (author is null)
            throw new ArgumentNullException(nameof(author));
        
        return _repository.GetBookByAuthor(author);
    }

   
}