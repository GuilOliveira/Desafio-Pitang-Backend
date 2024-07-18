using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository;
using System;
using System.Linq;

namespace DesafioPitang.UnitTests
{
    public static class DatabaseSeeder
    {
        public static void Seed(Context context)
        {
            if (!context.Usuario.Any())
            {
                var users = new[]
                {
                new User
                {
                    Email = "gui@pitang.com",
                    Name = "Guilherme",
                    PasswordHash = new byte[64],
                    PasswordSalt = new byte[128],
                    Profile = "admin",
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Email = "mariana@mail.com",
                    Name = "Mariana",
                    PasswordHash = new byte[64],
                    PasswordSalt = new byte[128],
                    Profile = "comum",
                    CreatedAt = DateTime.Now
                }
            };

                context.Usuario.AddRange(users);
            }
            if (!context.Paciente.Any())
            {
                var patients = new[]
                {
                new Patient { Name = "Danilo Queiroga", BirthDate = new DateTime(2000, 1, 1), CreatedAt = DateTime.Now },
                new Patient { Name = "Leandro Torres", BirthDate = new DateTime(2000, 5, 20), CreatedAt = DateTime.Now },
            };

                context.Paciente.AddRange(patients);
            }

            if (!context.Agendamento.Any())
            {
                var appointments = new[]
                {
                new Appointment { Date = DateTime.Today, Time = new TimeSpan(5,0,0), CreatedAt = DateTime.Today, Status = "Waiting", PatientId = 1 },
                new Appointment { Date = DateTime.Today.AddDays(1), Time = new TimeSpan(20,0,0), CreatedAt = DateTime.Today, Status = "Waiting", PatientId = 1 },
                new Appointment { Date = DateTime.Today.AddDays(2), Time = new TimeSpan(13,0,0), CreatedAt = DateTime.Today, Status = "Waiting", PatientId = 2 },
                new Appointment { Date = DateTime.Today.AddDays(3), Time = new TimeSpan(17,0,0), CreatedAt = DateTime.Today, Status = "Waiting", PatientId = 2 },
            };

                context.Agendamento.AddRange(appointments);
            }

            context.SaveChanges();
        }
    }
}
