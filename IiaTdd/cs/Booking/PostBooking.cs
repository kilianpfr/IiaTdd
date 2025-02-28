using IiaTdd.cs.Interface;

namespace IiaTdd.cs.Booking;

public class PostBooking : IMemberRepository
{
    private readonly IMemberRepository _repository;
    
    public PostBooking(IMemberRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public void BookingBook(int idBook, int idMember, int numberMonth, bool getmail)
    {
        _repository.BookingBook(idBook, idMember, numberMonth, getmail);
    }
}