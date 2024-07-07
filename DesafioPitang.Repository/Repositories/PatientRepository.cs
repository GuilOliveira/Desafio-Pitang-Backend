using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepositories;

namespace DesafioPitang.Repository.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(Context context) : base(context)
        {
        }
    }
}
