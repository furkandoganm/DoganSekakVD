using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class ProductionFacilityModel: Record
    {
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(30, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("Tesis İsmi")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(400, ErrorMessage = "{0} en fazla {1} karakter ile ifade edilmelidir!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(8, ErrorMessage = "{0} en az {1} karakter ile ifade edilmelidir!")]
        [DisplayName("Lokasyon")]
        public string Location { get; set; }
        public int Capacit { get; set; }
        public string ImagePath { get; set; }


        public int CityId { get; set; }
        public int DistrictId { get; set; }


        public List<ProductionFacilityProductModel> Products { get; set; }
        public CityModel City { get; set; }
        public DistrictModel District { get; set; }
    }
}
