using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.Entities
{
    public class Paciente : IdEntity<int>
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }

        public List<Agendamento> Agendamentos { get; set; }

        public Paciente() { }
    }
}
