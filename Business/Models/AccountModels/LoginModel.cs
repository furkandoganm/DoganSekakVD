using AppCore.Records;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models.AccountModels
{
    public class LoginModel: Record
    {
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(25, ErrorMessage = "{0} ismi en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(2, ErrorMessage = "{0} ismi en az {1} karakter olmalıdır!")]
        [DisplayName("E-Mail")]
        public string EMail { get; set; }
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(8, ErrorMessage = "{0} ismi en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(4, ErrorMessage = "{0} ismi en az {1} karakter olmalıdır!")]
        [DisplayName("Şifre")]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
    }
}
