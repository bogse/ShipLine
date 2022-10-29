namespace ShipLine.Models
{
    public class PortModel
    {
        public Guid PortId { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
