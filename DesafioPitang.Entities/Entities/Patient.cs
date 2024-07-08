namespace DesafioPitang.Entities.Entities
{
    public class Patient : IdEntity<int>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Appointment> Appointments { get; set; }

        public Patient() { }
    }
}
