namespace DesafioPitang.Entities.Entities
{
    public class Agendamento : IdEntity<int>
    {
        public int IdPaciente { get; set; }
        public DateOnly Data { get; set; }
        public TimeSpan Hora { get; set; }
        public string Status { get; set; }
        public DateTime DataCriacao { get; set; }

        public Paciente Paciente { get; set; }

        public Agendamento() { }
    }
}
