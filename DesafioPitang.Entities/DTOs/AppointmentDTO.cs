namespace DesafioPitang.Entities.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public string PatientName { get; set; }
        public int UserId { get; set; }
    }
}
