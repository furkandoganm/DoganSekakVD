using AppCore.Records;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class Review: Record
    {
        [Required]
        [StringLength(600)]
        public string Explanation { get; set; }
        public int NumberofLikes { get; set; }
        public int NumberofDislikes { get; set; }
        public bool IsActive { get; set; }


        public int UserId { get; set; }
        public int ProductId { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
