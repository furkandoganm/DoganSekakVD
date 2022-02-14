using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class ProductionFacilityRepositoryBase: RepositoryBase<ProductionFacility>
    {
        public ProductionFacilityRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
