using AppCore.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class Product: Record
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Explanation { get; set; }
        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; }
        public float Price { get; set; }
        public float ProductionCost { get; set; }
        public DateTime ProductionDate { get; set; }
        public string ImagePath { get; set; }
        public int StockAmount { get; set; }
        public int NumberofVisitsPerMonth { get; set; }
        public bool IsActive { get; set; }


        public int CategoryId { get; set; }


        public List<UserProduct> Users { get; set; }
        public List<ProductionFacilityProduct> ProductionFacilities { get; set; }
        public Category Category { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
