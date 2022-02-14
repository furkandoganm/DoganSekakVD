using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class DistrictRepository : DistrictRepositoryBase
    {
        public DistrictRepository(DbContext db) : base(db)
        {

        }
    }
}
