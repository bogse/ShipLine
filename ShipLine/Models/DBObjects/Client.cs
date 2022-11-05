using System;
using System.Collections.Generic;

namespace ShipLine.Models.DBObjects
{
    public partial class Client
    {
        public Client()
        {
            Shipments = new HashSet<Shipment>();
        }

        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
