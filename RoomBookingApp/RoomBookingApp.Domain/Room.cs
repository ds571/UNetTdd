namespace RoomBookingApp.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<RoomBooking> RoomBookings { get; set; }
    }
}