using DesafioPitang.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioPitang.Repository.Map
{
    public class AppointmentMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("tb_agendamento");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id_agendamento")
                .IsRequired();

            builder.Property(a => a.PatientId)
                .HasColumnName("id_paciente")
                .IsRequired();

            builder.Property(a => a.Date)
                .HasColumnName("dat_agendamento")
                .IsRequired();

            builder.Property(a => a.Time)
                .HasColumnName("hor_agendamento")
                .IsRequired();

            builder.Property(a => a.Status)
                .HasColumnName("dsc_status")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.CreatedAt)
                .HasColumnName("dat_criacao")
                .IsRequired();

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(x => x.PatientId)
                   .HasConstraintName("fk_agendamento_paciente");
        }
    }
}
