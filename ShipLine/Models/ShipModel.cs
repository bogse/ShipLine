namespace ShipLine.Models
{
    public class ShipModel
    {
        public Guid ShipId { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
    }
}
