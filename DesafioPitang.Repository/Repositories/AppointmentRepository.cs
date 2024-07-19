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

        public async new Task<List<Appointment>> GetAll()
        {
            var query = EntitySet.Include(appointment => appointment.Patient)
                .OrderBy(appointment => appointment.Date);
                
            return await query.ToListAsync();
        }

        public async Task<int> GetAmountByDate(DateTime date)
        {
            var query = EntitySet.Where(appointment => appointment.Date.Date == date.Date);
                                  
            return await query.CountAsync();

        }

        public async Task<int> GetAmountByTime(DateTime date, TimeSpan time)
        {
            var query = EntitySet.Where(appointment => appointment.Date.Date == date.Date && 
                                                       appointment.Time == time);
            return await query.CountAsync();
        }

        public async Task<int> GetUserId(int appointmentId)
        {
            var appointment = await EntitySet.Include(appointment => appointment.Patient).
                FirstAsync(appointment => appointment.Id == appointmentId);

            return appointment.Patient.UserId;
        }

        public async Task<List<Appointment>> GetAllByUser(int userId)
        {
            var query = EntitySet.Include(appointment => appointment.Patient)
                                  .Where(appointment => (appointment.Patient.UserId == userId))
                                  .OrderBy(appointment => appointment.Date);

            return await query.ToListAsync();
        }
    }
}
