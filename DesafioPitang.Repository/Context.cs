using DesafioPitang.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioPitang.Repository
{
    public class Context : DbContext
    {
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}