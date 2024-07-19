using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Utils.Constants;
using DesafioPitang.Utils.Extensions;
using DesafioPitang.Utils.Helpers;
using DesafioPitang.Utils.UserContext;
using DesafioPitang.Validators;

namespace DesafioPitang.Business.Businesses
{
    public class AppointmentBusiness : IAppointmentBusiness
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserContext _userContext;
        public AppointmentBusiness(IAppointmentRepository appointmentRepository, IUserContext userContext)
        {
            _appointmentRepository = appointmentRepository;
            _userContext = userContext;
        }        

        public async Task<List<List<AppointmentDTO>>> GetAll()
        {
            var allAppointments = await _appointmentRepository.GetAll();
            return GroupEntities.GroupAppointmentsByDate(allAppointments);
            
        }

        public async Task<List<List<AppointmentDTO>>> GetByDate(DateTime initialDate, DateTime finalDate)
        {
            AppointmentValidator.ValidateGetByDate(initialDate, finalDate);

            var appointmentsByDay = await _appointmentRepository.GetAllByDate(initialDate, finalDate);
            return GroupEntities.GroupAppointmentsByDate(appointmentsByDay);
        }

        public async Task<List<List<AppointmentDTO>>> GetAllByUser()
        {
            var currentUserId = UserContextExtensions.Id(_userContext);
            var appointmentsByUser = await _appointmentRepository.GetAllByUser(currentUserId);
            return GroupEntities.GroupAppointmentsByDate(appointmentsByUser);
        }

        public async Task<AppointmentDTO> UpdateStatus(AppointmentStatusUpdateModel statusModel)
        {
            AppointmentValidator.EnsureAppointmentExists(await _appointmentRepository.GetById(statusModel.Id) !=null);
            AppointmentValidator.ValidateStatusChange(statusModel);
            AppointmentValidator.EnsureUserIsAuthorized(currentUserId: UserContextExtensions.Id(_userContext),
                                                        UserId: await _appointmentRepository.GetUserId(statusModel.Id),
                                                        profile: UserContextExtensions.Profile(_userContext));

            var appointment = await _appointmentRepository.ChangeStatus(statusModel);
            return new AppointmentDTO
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Status = appointment.Status,
                PatientName = appointment.Patient?.Name,
                UserId = appointment.Patient.UserId
            };
        }
        public async Task DeleteById(int id)
        {
            AppointmentValidator.EnsureAppointmentExists(await _appointmentRepository.GetById(id) != null);
            AppointmentValidator.EnsureUserIsAuthorized(currentUserId: UserContextExtensions.Id(_userContext),
                                                        UserId: await _appointmentRepository.GetUserId(id),
                                                        profile: UserContextExtensions.Profile(_userContext));

            await _appointmentRepository.DeleteById(id);
        }
    }
}
