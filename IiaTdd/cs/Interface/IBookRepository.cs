using IiaTdd.objet;

namespace IiaTdd.cs.Interface;

public interface IBookRepository
{
    objet.PostBookObj GetBookByIsbnForPost(string isbn);
    bool DeleteBookById(int id);


    void UpdateBookById(int id, objet.PostBookObj bookObj);
    
    void AddBook(PostBookObj bookObj);
    
    List<GetBookObj> GetBookByAuthor(objet.Author author);
    
}