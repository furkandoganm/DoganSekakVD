using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class City: Record
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public List<District> Districts { get; set; }
        public List<User> Users { get; set; }
    }
}
