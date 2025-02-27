using IiaTdd.objet;

namespace IiaTdd.cs.Interface;

public interface IBookRepository
{
    PostBook GetBookByIsbn(string isbn);
    Boolean DeleteBookByIsbn(int id);
}