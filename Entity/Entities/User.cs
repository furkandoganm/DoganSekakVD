using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class User: Record
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [StringLength(40)]
        public string Surname { get; set; }
        [StringLength(11)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(40)]
        public string EMail { get; set; }
        [Required]
        [StringLength(8)]
        public string Password { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public int VisitFrequency { get; set; }
        public bool IsActive { get; set; }


        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int RoleId { get; set; }
        public int PostNumberId { get; set; }


        public City City { get; set; }
        public District District { get; set; }
        public List<Review> Reviews { get; set; }
        public List<UserProduct> Products { get; set; }
        public Role Role { get; set; }
        public PostNumber PostNumber { get; set; }
    }
}
