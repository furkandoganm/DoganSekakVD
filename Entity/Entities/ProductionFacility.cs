using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class ProductionFacility: Record
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [StringLength(400)]
        public string Location { get; set; }
        public int Capacit { get; set; }
        public string ImagePath { get; set; }


        public int CityId { get; set; }
        public int DistrictId { get; set; }


        public List<ProductionFacilityProduct> Products { get; set; }
        public City City { get; set; }
        public District District { get; set; }
    }
}
