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
            var appointment = await EntitySet.Include(appointment => appointment.Patient)
                                             .FirstAsync(a => a.Id== statusModel.Id);
            appointment.Status = statusModel.Status;
            return appointment;
        }

        public async Task<List<Appointment>> GetAllByDate(DateTime initialDate, DateTime finalDate)
        {
            var query = EntitySet.Include(appointment => appointment.Patient)
                                  .Where(appointment => (appointment.Date >= initialDate.Date &&
                                                        appointment.Date <= finalDate.Date))
                                  .OrderBy(appointment => appointment.Date);
            return await query.ToListAsync();
        }

        public new Task<List<Appointment>> GetAll()
        {
            return EntitySet.Include(appointment => appointment.Patient)
                .OrderBy(appointment => appointment.Date)
                .ToListAsync();
        }
    }
}
