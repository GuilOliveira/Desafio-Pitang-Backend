namespace DesafioPitang.Entities.Models
{
    public class SchedulingModel
    {
        public string PatientName { get; set; }
        public DateTime PatientBirthDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
    }
}
