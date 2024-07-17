using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Utils.Constants;
using DesafioPitang.Utils.Extensions;
using DesafioPitang.Utils.UserContext;
using DesafioPitang.Validators;

namespace DesafioPitang.Business.Businesses
{
    public class SchedulingBusiness : ISchedulingBusiness
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        public SchedulingBusiness(IPatientRepository patientRepository, 
                                  IAppointmentRepository appointmentRepository,
                                  IUserContext userContext,
                                  IUserRepository userRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _userContext = userContext;
            _userRepository = userRepository;
        }

        public async Task<AppointmentDTO> Post(SchedulingModel schedulingModel)
        {
            ScheduleValidator.ValidatePostFields(schedulingModel);
            ScheduleValidator.ValidatePostAvailability(
                await _appointmentRepository.GetAmountByDate(schedulingModel.AppointmentDate),
                await _appointmentRepository.GetAmountByTime(schedulingModel.AppointmentDate, schedulingModel.AppointmentTime));

            var user = await _userRepository.GetById(UserContextExtensions.Id(_userContext));
            var patient = await _patientRepository.GetByName(schedulingModel.PatientName);

            if (patient == null || 
                patient.BirthDate != schedulingModel.PatientBirthDate || 
                patient.UserId != user.Id )
                {
                    patient = await _patientRepository.Insert(new Patient
                    {
                        Name = schedulingModel.PatientName,
                        BirthDate = schedulingModel.PatientBirthDate,
                        CreatedAt = DateTime.Now,
                        UserId = user.Id,
                    });
                }
            var appointment = await _appointmentRepository.Insert(new Appointment
            {
                Date = schedulingModel.AppointmentDate,
                Time = schedulingModel.AppointmentTime,
                CreatedAt = DateTime.Now,
                Status = AppointmentStatusConstants.WAITING,
                PatientId = patient.Id,
                Patient = patient
            });

            return new AppointmentDTO
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Status = appointment.Status,
                PatientName = patient.Name,
                UserId = user.Id
            };
        }
    }
}
