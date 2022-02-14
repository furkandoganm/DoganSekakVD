using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Business.Models
{
    public class UserModel : Record 
    {
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(30, ErrorMessage = "{0} en fazla {1} karakter ile ifade edilmelidir!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter ile ifade edilmelidir!")]
        [DisplayName("İsim")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(30, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("Soy İsim")]
        public string Surname { get; set; }
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(11, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(9, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(30, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(4, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [EmailAddress]
        [DisplayName("E-Mail")]
        public string EMail { get; set; }
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(10, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(4, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("Şifre")]
        public string Password { get; set; }
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(400, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(15, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("Adres")]
        public string Address { get; set; }
        public int VisitFrequency { get; set; }
        public bool IsActive { get; set; }


        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int RoleId { get; set; }
        public int PostNumberId { get; set; }


        public CityModel City { get; set; }
        public DistrictModel District { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public List<UserProductModel> Products { get; set; }
        public RoleModel Role { get; set; }
        public PostNumberModel PostNumber { get; set; }
    }
}
