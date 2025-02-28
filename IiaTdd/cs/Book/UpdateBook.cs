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

    public bool UpdateBookIdRep(int id, objet.PostBookObj bookObj)
    {
        _repository.UpdateBookById(id, bookObj);
        return true;
    }
}