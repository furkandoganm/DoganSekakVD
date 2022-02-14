using DataAccess.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductRepository: ProductRepositoryBase
    {
        public ProductRepository(DbContext db): base(db)
        {

        }
    }
}
