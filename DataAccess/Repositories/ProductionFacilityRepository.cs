using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductionFacilityRepository: ProductionFacilityRepositoryBase
    {
        public ProductionFacilityRepository(DbContext db): base(db)
        {

        }
    }
}
