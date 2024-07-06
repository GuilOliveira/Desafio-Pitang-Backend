using DesafioPitang.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioPitang.Repository.Map
{
    public class PatientMap : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("tb_paciente");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id_paciente")
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("dsc_nome")
                .IsRequired();

            builder.Property(p => p.BirthDate)
                .HasColumnName("dat_nascimento")
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .HasColumnName("dat_criacao")
                .IsRequired();

            builder.HasMany(p => p.Appointments)
                   .WithOne(a => a.Patient)
                   .HasForeignKey(a => a.PatientId)
                   .HasConstraintName("fk_agendamento_paciente");
        }
    }
}
