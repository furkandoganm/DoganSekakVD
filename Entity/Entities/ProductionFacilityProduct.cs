using AppCore.Records;

namespace Entity.Entities
{
    public class ProductionFacilityProduct: Record
    {
        public int ProductId { get; set; }
        public int ProductionFacilityId { get; set; }


        public Product Product { get; set; }
        public ProductionFacility ProductionFacility { get; set; }
    }
}
