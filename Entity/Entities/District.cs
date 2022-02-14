using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class District: Record
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }


        public int CityId { get; set; }


        public List<User> Users { get; set; }
        public List<ProductionFacility> ProductionFacilities { get; set; }
        public City City { get; set; }
    }
}
