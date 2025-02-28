namespace IiaTdd.cs.Interface;

public interface IMemberRepository
{
    void BookingBook(int idBook, int idMember, int numberMonth, bool getmail);
}