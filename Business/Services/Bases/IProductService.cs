using AppCore.Bussiness.Models.Results;
using AppCore.Bussiness.Services.Bases;
using Business.Models;

namespace Business.Services.Bases
{
    public interface IProductService: IService<ProductModel>
    {
        Result FakeDelete(string guId);
    }
}
