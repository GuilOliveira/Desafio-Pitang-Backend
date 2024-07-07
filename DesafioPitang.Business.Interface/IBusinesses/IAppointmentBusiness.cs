using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;

namespace DesafioPitang.Business.Interface.IBusinesses
{
    public interface IAppointmentBusiness
    {
        Task<List<AppointmentDTO>> GetAll();
        Task<List<AppointmentDTO>> GetByDate(DateTime date);
        Task<AppointmentDTO> UpdateStatus(AppointmentStatusUpdateModel statusModel);
    }
}
