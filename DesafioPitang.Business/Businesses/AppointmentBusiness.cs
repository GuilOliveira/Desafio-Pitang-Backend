using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Utils.Helpers;

namespace DesafioPitang.Business.Businesses
{
    public class AppointmentBusiness : IAppointmentBusiness
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentBusiness(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task DeleteById(int id)
        {
            await _appointmentRepository.DeleteById(id);
        }

        public async Task<List<List<AppointmentDTO>>> GetAll()
        {
            var allAppointments = await _appointmentRepository.GetAll();
            return GroupEntities.GroupAppointmentsByDate(allAppointments);
            
        }

        public async Task<List<List<AppointmentDTO>>> GetByDate(DateTime initialDate, DateTime finalDate)
        {
            var appointmentsByDay = await _appointmentRepository.GetAllByDate(initialDate, finalDate);
            return GroupEntities.GroupAppointmentsByDate(appointmentsByDay);
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
