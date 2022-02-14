using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductionFacilityProductRepository: ProductionFacilityProductRepositoryBase
    {
        public ProductionFacilityProductRepository(DbContext db): base(db)
        {

        }
    }
}
