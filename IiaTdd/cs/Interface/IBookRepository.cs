using IiaTdd.objet;

namespace IiaTdd.cs.Interface;

public interface IBookRepository
{
    PostBook GetBookByISBN(string isbn);
}