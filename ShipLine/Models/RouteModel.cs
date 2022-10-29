namespace ShipLine.Models
{
    public class RouteModel
    {
        public Guid RouteId { get; set; }
        public Guid SourcePortId { get; set; }
        public Guid DestinationPortId { get; set; }
        public string Name { get; set; } = null!;
    }
}
