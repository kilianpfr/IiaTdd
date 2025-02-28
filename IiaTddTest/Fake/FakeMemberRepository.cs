using IiaTdd.cs.Book;
using IiaTdd.cs.Interface;
using IiaTdd.objet;

namespace IiaTddTest.Fake;

public class FakeMemberRepository : IMemberRepository
{
 
    public List<Member> _members = new List<Member>()
    {
        new Member()
        {
            IdMember = "1",
            Name = "Doe",
            FirstName = "John",
            BirthDate = "01/01/2000",
            Civility = "Mr"
        },
        new Member()
        {
            IdMember = "2",
            Name = "Doe",
            FirstName = "Jane",
            BirthDate = "01/01/2000",
            Civility = "Mrs"
        },
        new Member()
        {
            IdMember = "3",
            Name = "Doe",
            FirstName = "Jack",
            BirthDate = "01/01/2000",
            Civility = "Mr"
        }
    };

    private List<GetLink> _links = new();
    
    private List<GetLink> _history = new();
    


    public void BookingBook(int idBook, int idMember, int numberMonth, bool getmail)
    {
        var fakebook = new FakeBookRepository();
        List<GetBookObj> books = fakebook.GetBooks();
        var book = books.Find(x => x.Id == idBook);
        var member = _members.Find(x => x.IdMember == idMember.ToString());
        if (book == null || member == null)
        {
            throw new Exception("Livre ou membre non trouvé");
        }
        if(_links.Where(x =>   x.IdMember == idMember).ToList().Count >= 3)
        {
            throw new Exception("Ce membre a déjà réservé 3 livres");
        }
        _links.Add(new GetLink()
        {
            Id = _links.Count + 1,
            IdBook = idBook,
            IdMember = idMember,
            Numbermonth = numberMonth,
            Getmail = getmail
        });
    }

    public void StopBookingBook(int idBook, int idMember)
    {
        
        var link = _links.Find(x => x.IdBook == idBook && x.IdMember == idMember);
        if (link == null)
        {
            throw new Exception("Livre ou membre non trouvé");
        }
        _history.Add(link);
       
        _links.Remove(link);
    }

    public List<GetBookObj> GetHistoryAuthor(int member)
    {
        List<int> idBooks = _history.Where(x => x.IdMember == member).Select(x => x.IdBook).ToList();
        var fakebook = new FakeBookRepository();
        List<GetBookObj> books = fakebook.GetBooks();
        return books.Where(x => idBooks.Contains(x.Id)).ToList();
    }
}