using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;

namespace DesafioPitang.Business.Interface.IBusinesses
{
    public interface ISchedulingBusiness
    {
        Task<AppointmentDTO> Post(SchedulingModel schedulingModel);
    }
}
