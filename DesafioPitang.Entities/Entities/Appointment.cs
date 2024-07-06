namespace DesafioPitang.Entities.Entities
{
    public class Appointment : IdEntity<int>
    {
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Patient Patient { get; set; }

        public Appointment() { }
    }
}
