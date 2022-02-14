using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class ProductionFacilityProductRepositoryBase: RepositoryBase<ProductionFacilityProduct>
    {
        public ProductionFacilityProductRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
