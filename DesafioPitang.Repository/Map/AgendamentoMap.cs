using DesafioPitang.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioPitang.Repository.Map
{
    public class AgendamentoMap : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("tb_agendamento");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id_agendamento")
                .IsRequired();

            builder.Property(a => a.IdPaciente)
                .HasColumnName("id_paciente")
                .IsRequired();

            builder.Property(a => a.Data)
                .HasColumnName("dat_agendamento")
                .IsRequired();

            builder.Property(a => a.Hora)
                .HasColumnName("hor_agendamento")
                .IsRequired();

            builder.Property(a => a.Status)
                .HasColumnName("dsc_status")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.DataCriacao)
                .HasColumnName("dat_criacao")
                .IsRequired();

            builder.HasOne(a => a.Paciente)
                   .WithMany(p => p.Agendamentos)
                   .HasForeignKey(x => x.IdPaciente)
                   .HasConstraintName("fk_agendamento_paciente");
        }
    }
}
