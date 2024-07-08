using DesafioPitang.Entities.Entities;

namespace DesafioPitang.Repository.Interface.IRepositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<Patient> GetByName(string name);
    }
}
