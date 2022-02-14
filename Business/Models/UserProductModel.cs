using AppCore.Records;
namespace Business.Models
{
    public class UserProductModel : Record 
    {
        public bool IsLike { get; set; }
        public bool IsBuy { get; set; }


        public int ProductId { get; set; }
        public int UserId { get; set; }


        public ProductModel Product { get; set; }
        public UserModel User { get; set; }
    }
}
