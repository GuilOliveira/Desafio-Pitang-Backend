using DesafioPitang.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioPitang.Repository
{
    public class Context : DbContext
    {
        public DbSet<Appointment> Agendamento { get; set; }
        public DbSet<Patient> Paciente { get; set; }
        public DbSet<User> Usuario { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}