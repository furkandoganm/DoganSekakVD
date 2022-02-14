using AppCore.DataAccess.Repositories.EntityFramework;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Bases
{
    public class ProductRepositoryBase: RepositoryBase<Product>
    {
        public ProductRepositoryBase(DbContext db): base(db)
        {

        }
    }
}
