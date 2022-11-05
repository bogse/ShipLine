using System;
using System.Collections.Generic;

namespace ShipLine.Models.DBObjects
{
    public partial class Voyage
    {
        public Voyage()
        {
            VoyageShipments = new HashSet<VoyageShipment>();
        }

        public Guid VoyageId { get; set; }
        public Guid ShipId { get; set; }
        public Guid RouteId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int VoyageQuantity { get; set; }
        public int CostPerTeq { get; set; }
        public int VoyageNumber { get; set; }

        public virtual Route Route { get; set; } = null!;
        public virtual Ship Ship { get; set; } = null!;
        public virtual ICollection<VoyageShipment> VoyageShipments { get; set; }
    }
}
