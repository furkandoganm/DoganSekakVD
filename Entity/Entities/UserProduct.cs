using AppCore.Records;

namespace Entity.Entities
{
    public class UserProduct: Record
    {
        public bool IsLike { get; set; }
        public bool IsBuy { get; set; }


        public int ProductId { get; set; }
        public int UserId { get; set; }


        public Product Product { get; set; }
        public User User { get; set; }
    }
}
