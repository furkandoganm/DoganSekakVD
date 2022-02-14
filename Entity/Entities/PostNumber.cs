using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class PostNumber: Record
    {
        [Required]
        [StringLength(10)]
        public string Number { get; set; }

        public List<User> Users { get; set; }
    }
}
