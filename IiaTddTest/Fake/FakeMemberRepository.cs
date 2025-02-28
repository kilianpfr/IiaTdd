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
      
       
        _links.Add(new GetLink()
        {
            Id = _links.Count + 1,
            IdBook = idBook,
            IdMember = idMember,
            Numbermonth = numberMonth,
            Getmail = getmail
        });
        
        
    }
}