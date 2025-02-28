using IiaTdd.objet;

namespace IiaTdd.cs.Interface;

public interface IMemberRepository
{
    void BookingBook(int idBook, int idMember, int numberMonth, bool getmail);
    void StopBookingBook(int idBook, int idMember);
    List<GetBookObj> GetHistoryAuthor(int idMember);
    
    List<GetLink> GetLinks();
    
    List<GetBookObj> SendMail(int monthpassed);
}