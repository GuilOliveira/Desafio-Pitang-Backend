using DesafioPitang.Entities.Entities;

namespace DesafioPitang.Repository.Interface.IRepository
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<List<Appointment>> GetAllByDate(DateOnly date);
        Task<List<Appointment>> GetAllByDatetime(DateTime date);
        Task<Appointment> ChangeStatus(string Status);
    }
}
