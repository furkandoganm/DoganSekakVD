using AppCore.Records;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class Category: Record
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
