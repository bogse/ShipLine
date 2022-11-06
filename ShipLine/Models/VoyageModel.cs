namespace ShipLine.Models
{
    public class VoyageModel
    {
        public Guid VoyageId { get; set; }
        public Guid ShipId { get; set; }
        public Guid RouteId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int VoyageQuantity { get; set; }
        public int CostPerTeq { get; set; }
        public int VoyageNumber { get; set; }
    }
}
