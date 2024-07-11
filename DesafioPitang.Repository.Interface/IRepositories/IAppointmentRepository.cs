using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Models;

namespace DesafioPitang.Repository.Interface.IRepositories
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<List<Appointment>> GetAllByDate(DateTime initialDate, DateTime finalDate);
        Task<List<Appointment>> GetAll();
        Task<Appointment> ChangeStatus(AppointmentStatusUpdateModel statusModel);
    }
}
