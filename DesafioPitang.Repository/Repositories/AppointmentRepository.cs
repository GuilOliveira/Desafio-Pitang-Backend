using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepository;


namespace DesafioPitang.Repository.Repositories
{
    internal class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(Context context) : base(context)
        {
        }

        public Task<Appointment> ChangeStatus(string Status)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetAllByDate(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetAllByDatetime(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
