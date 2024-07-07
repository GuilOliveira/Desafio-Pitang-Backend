using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;

namespace DesafioPitang.Business.Businesses
{
    public class AppointmentBusiness : IAppointmentBusiness
    {
        public Task<List<AppointmentDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<AppointmentDTO>> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentDTO> UpdateStatus(AppointmentStatusUpdateModel statusModel)
        {
            throw new NotImplementedException();
        }
    }
}
