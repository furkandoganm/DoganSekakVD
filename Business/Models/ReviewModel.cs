using AppCore.Records;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Business.Models
{
    public class ReviewModel : Record 
    {
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(400, ErrorMessage = "{0} en fazla {1} karakter ile ifade edilmelidir!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter ile ifade edilmelidir!")]
        [DisplayName("Yorum")]
        public string Explanation { get; set; }
        public int NumberofLikes { get; set; }
        public int NumberofDislikes { get; set; }
        public bool IsActive { get; set; }


        public int UserId { get; set; }
        public int ProductId { get; set; }

        public UserModel User { get; set; }
        public ProductModel Product { get; set; }
    }
}
