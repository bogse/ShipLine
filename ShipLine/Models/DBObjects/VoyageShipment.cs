using System;
using System.Collections.Generic;

namespace ShipLine.Models.DBObjects
{
    public partial class VoyageShipment
    {
        public Guid VoyageShipmentId { get; set; }
        public Guid ShipmentId { get; set; }
        public Guid VoyageId { get; set; }

        public virtual Shipment Shipment { get; set; } = null!;
        public virtual Voyage Voyage { get; set; } = null!;
    }
}
