using AppCore.Records;

namespace Business.Models
{
    public class ProductionFacilityProductModel: Record
    {
        public int ProductId { get; set; }
        public int ProductionFacilityId { get; set; }


        public ProductModel Product { get; set; }
        public ProductionFacilityModel ProductionFacility { get; set; }
    }
}
