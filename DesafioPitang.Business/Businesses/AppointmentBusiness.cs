using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository.Interface.IRepositories;

namespace DesafioPitang.Business.Businesses
{
    public class AppointmentBusiness : IAppointmentBusiness
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentBusiness(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<AppointmentDTO>> GetAll()
        {
            var allAppointments = await _appointmentRepository.GetAll();
            return allAppointments.Select(appointment => new AppointmentDTO
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Status = appointment.Status,
                PatientName = appointment.Patient?.Name
            }).ToList();
        }

        public async Task<List<AppointmentDTO>> GetByDate(DateTime date)
        {
            var appointmentsByDay = await _appointmentRepository.GetAllByDate(date);
            return appointmentsByDay.Select(appointment => new AppointmentDTO
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Status = appointment.Status,
                PatientName = appointment.Patient?.Name
            }).ToList();
        }

        public async Task<AppointmentDTO> UpdateStatus(AppointmentStatusUpdateModel statusModel)
        {
            var appointment = await _appointmentRepository.ChangeStatus(statusModel);
            return new AppointmentDTO
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Status = appointment.Status,
                PatientName = appointment.Patient?.Name
            };
        }
    }
}
