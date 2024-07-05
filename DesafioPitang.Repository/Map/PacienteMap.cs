using DesafioPitang.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioPitang.Repository.Map
{
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("tb_paciente");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_paciente")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("dsc_nome")
                .IsRequired();

            builder.Property(p => p.DataNascimento)
                .HasColumnName("dat_nascimento")
                .IsRequired();

            builder.Property(p => p.DataCriacao)
                .HasColumnName("dat_criacao")
                .IsRequired();

            builder.HasMany(p => p.Agendamentos)
                   .WithOne(a => a.Paciente)
                   .HasForeignKey(a => a.IdPaciente)
                   .HasConstraintName("fk_agendamento_paciente");
        }
    }
}
