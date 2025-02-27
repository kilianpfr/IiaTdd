using IiaTdd.cs.Interface;
using IiaTdd.objet;

namespace IiaTdd.cs.Book;

public class UpdateBook 
{
    public readonly IBookRepository _repository;
    
    public UpdateBook(IBookRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public bool UpdateBookIdRep(int id, PostBook book)
    {
        _repository.UpdateBookById(id, book);
        return true;
    }
}