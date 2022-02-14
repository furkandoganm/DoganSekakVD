using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class Role: Record
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }


        public List<User> Users { get; set; }
    }
}
