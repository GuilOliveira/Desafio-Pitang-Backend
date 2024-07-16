namespace DesafioPitang.Entities.Entities
{
    public class Patient : IdEntity<int>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

        public List<Appointment> Appointments { get; set; }
        public User User { get; set; }

        public Patient() { }
    }
}
