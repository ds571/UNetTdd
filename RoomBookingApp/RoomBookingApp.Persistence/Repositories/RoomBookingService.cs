using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingAppDbContext _context;
        public RoomBookingService(RoomBookingAppDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            // var unavailableRooms = _context.RoomBookings.Where(q => q.Date == date).Select(q => q.RoomId).ToList();
            // var availableRooms = _context.Rooms.Where(q => !unavailableRooms.Contains(q.Id)).ToList();
            return _context.Rooms.Where(q => !(q.RoomBookings.Any(x => x.Date == date))).ToList();
        }

        public void Save(RoomBooking roomBooking)
        {
            _context.Add(roomBooking);
            _context.SaveChanges();
        }
    }
}
