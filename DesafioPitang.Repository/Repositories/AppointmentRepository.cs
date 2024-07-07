using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioPitang.Repository.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(Context context) : base(context)
        {
        }

        public async Task<Appointment> ChangeStatus(AppointmentStatusUpdateModel statusModel)
        {
            var appointment = await GetById(statusModel.Id);
            appointment.Status = statusModel.Status;
            return appointment;
        }

        public async Task<List<Appointment>> GetAllByDate(DateTime date)
        {
            var query = EntitySet.Where(appointment => appointment.Date == date.Date);
            return await query.ToListAsync();
        }

        public Task<List<Appointment>> GetAllByDatetime(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
