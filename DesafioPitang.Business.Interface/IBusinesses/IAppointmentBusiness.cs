using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;

namespace DesafioPitang.Business.Interface.IBusinesses
{
    public interface IAppointmentBusiness
    {
        Task<List<List<AppointmentDTO>>> GetAll();
        Task<List<List<AppointmentDTO>>> GetByDate(DateTime initialDate, DateTime finalDate);
        Task<AppointmentDTO> UpdateStatus(AppointmentStatusUpdateModel statusModel);
        Task DeleteById(int id);
    }
}
