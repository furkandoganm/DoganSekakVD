﻿using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class CityModel: Record
    {
        //[Required(ErrorMessage = "{0} is required!")]
        [Required(ErrorMessage = "{0} alanının doldurulması zorunludur!")]
        //[MaxLength(25, ErrorMessage = "{0} must be maximum {1} character!")]
        [MaxLength(25, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        //[MinLength(2, ErrorMessage = "{0} must be minimum {1} character!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("Şehir İsmi")]
        public string Name { get; set; }

        public List<DistrictModel> Districts { get; set; }
        public List<UserModel> Users { get; set; }
    }
}
