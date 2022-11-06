namespace ShipLine.Models
{
    public class ClientModel
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
