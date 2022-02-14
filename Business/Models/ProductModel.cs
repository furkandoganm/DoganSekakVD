using AppCore.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class ProductModel : Record
    {
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(30, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("İsim")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(120, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("Açıklama")]
        public string Explanation { get; set; }
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(40, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("Seri Numarası")]
        public string SerialNumber { get; set; }
        public float Price { get; set; }
        public float ProductionCost { get; set; }
        public DateTime ProductionDate { get; set; }
        public string ImagePath { get; set; }
        public int StockAmount { get; set; }
        public int NumberofVisitsPerMonth { get; set; }
        public bool IsActive { get; set; }


        public int CategoryId { get; set; }


        public List<UserProductModel> Users { get; set; }
        public List<ProductionFacilityProductModel> ProductionFacilities { get; set; }
        public CategoryModel Category { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}
