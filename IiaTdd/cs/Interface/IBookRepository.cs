using IiaTdd.objet;

namespace IiaTdd.cs.Interface;

public interface IBookRepository
{
    PostBook GetBookByIsbn(string isbn);
    bool DeleteBookById(int id);


    void UpdateBookById(int id, PostBook book);
}