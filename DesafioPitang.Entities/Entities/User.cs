namespace DesafioPitang.Entities.Entities
{
    public class User : IdEntity<int>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Profile { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Patient> Patients { get; set; }

        public User() { }
    }
}