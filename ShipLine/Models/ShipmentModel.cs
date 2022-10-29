namespace ShipLine.Models
{
    public class ShipmentModel
    {
        public Guid ShipmentId { get; set; }
        public Guid CustomerId { get; set; }
        public string CargoContents { get; set; } = null!;
        public int QuantityTeq { get; set; }
        public DateTime ShipRequestDate { get; set; }
        public DateTime NeedByDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
