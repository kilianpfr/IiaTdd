using IiaTdd.cs.Interface;
using IiaTdd.objet;

namespace IiaTddTest.Fake;

public class FakeMemberRepository : IMemberRepository
{
    /*
     *   public string? IdMember { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? BirthDate { get; set; }
    public string? Civility { get; set; }
     */
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