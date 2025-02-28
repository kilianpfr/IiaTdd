using IiaTdd.cs.Interface;
using IiaTdd.objet;

namespace IiaTdd.cs.Booking;

public class Booking : IMemberRepository
{
    private readonly IMemberRepository _repository;
    
    public Booking(IMemberRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public void BookingBook(int idBook, int idMember, int numberMonth, bool getmail)
    {
        if (numberMonth > 4)
        {
            throw new Exception("La durée de réservation ne peut pas dépasser 4 mois");
        }
        _repository.BookingBook(idBook, idMember, numberMonth, getmail);
    }
    public void StopBookingBook(int idBook, int idMember)
    {
        _repository.StopBookingBook(idBook, idMember);
    }

    public List<GetBookObj> GetHistoryAuthor(int idMember)
    {
        return _repository.GetHistoryAuthor(idMember);
    }

    public List<GetLink> GetLinks()
    {
        return _repository.GetLinks();
    }
}