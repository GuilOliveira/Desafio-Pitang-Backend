using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository.Interface.IRepositories;

namespace DesafioPitang.Business.Businesses
{
    public class SchedulingBusiness : ISchedulingBusiness
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        public SchedulingBusiness(IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
        }
        public async Task<AppointmentDTO> Post(SchedulingModel schedulingModel)
        {
            var patient = await _patientRepository.GetByName(schedulingModel.PatientName);
            if (patient == null)
            {
                patient = await _patientRepository.Insert(new Patient
                {
                    Name = schedulingModel.PatientName,
                    BirthDate = schedulingModel.PatientBirthDate,
                    CreatedAt = DateTime.Now
                });
            }
            var appointment = await _appointmentRepository.Insert(new Appointment
            {
                Date = schedulingModel.AppointmentDate,
                Time = schedulingModel.AppointmentTime,
                CreatedAt = DateTime.Now,
                Status = "Waiting",
                PatientId = patient.Id,
                Patient = patient
            });
            return new AppointmentDTO
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Status = appointment.Status,
                PatientName = patient.Name
            };
        }
    }
}
