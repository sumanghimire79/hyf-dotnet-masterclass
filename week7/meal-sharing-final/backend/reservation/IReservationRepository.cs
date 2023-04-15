namespace mealSharingFfinal.reservation.IReservationRepository;
using mealSharingFfinal.reservation.Reservation;
public interface IReservationRepository
{
  Task<IEnumerable<Reservation>> GetAllReservation();
  Task<Object> GetReservationByID(int id);
  Task<Reservation> AddReservation(Reservation reservation);
  Task<Reservation> UpdateReservationByID(Reservation reservation, int id);
  Task<Object> DeleteReservationByID(int id);
  Task<IEnumerable<Reservation>> SearchReservation(string title);

}