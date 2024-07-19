using DesafioPitang.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioPitang.Repository.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tb_usuario");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id_usuario")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Email)
                .HasColumnName("dsc_email")
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Name)
                .HasColumnName("dsc_nome")
                .HasMaxLength(255)
                .IsRequired();


            builder.Property(u => u.PasswordHash)
                .HasColumnName("dsc_password_hash")
                .IsRequired();

            builder.Property(u => u.PasswordSalt)
                .HasColumnName("dsc_password_salt")
                .IsRequired();

            builder.Property(u => u.Profile)
                .HasColumnName("dsc_perfil")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnName("dat_criacao")
                .IsRequired();

            builder.HasMany(u => u.Patients)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
        }
    }
}