using System;
using System.Collections.Generic;

namespace ShipLine.Models.DBObjects
{
    public partial class Shipment
    {
        public Shipment()
        {
            VoyageShipments = new HashSet<VoyageShipment>();
        }

        public Guid ShipmentId { get; set; }
        public Guid CustomerId { get; set; }
        public string CargoContents { get; set; } = null!;
        public int QuantityTeq { get; set; }
        public DateTime ShipRequestDate { get; set; }
        public DateTime NeedByDate { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<VoyageShipment> VoyageShipments { get; set; }
    }
}
