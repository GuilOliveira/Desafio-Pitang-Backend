using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioPitang.Repository.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(Context context) : base(context)
        {
        }

        public async Task<Patient> GetByName(string name)
        {
            return await EntitySet.FirstOrDefaultAsync(patient => patient.Name == name);
        }
    }
}
