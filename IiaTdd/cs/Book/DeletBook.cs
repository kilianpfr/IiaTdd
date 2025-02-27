using IiaTdd.cs.Interface;

namespace IiaTdd.cs.Book;

public class DeleteBook
{
    private readonly IBookRepository _repository;
    
    public DeleteBook(IBookRepository repository)
    {
        _repository = repository  ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public bool DeleteBookIdRep(int id)
    {
        _repository.DeleteBookById(id);
        return true;
    }
}