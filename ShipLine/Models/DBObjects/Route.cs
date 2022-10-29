using System;
using System.Collections.Generic;

namespace ShipLine.Models.DBObjects
{
    public partial class Route
    {
        public Route()
        {
            Voyages = new HashSet<Voyage>();
        }

        public Guid RouteId { get; set; }
        public Guid SourcePortId { get; set; }
        public Guid DestinationPortId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Port DestinationPort { get; set; } = null!;
        public virtual Port SourcePort { get; set; } = null!;
        public virtual ICollection<Voyage> Voyages { get; set; }
    }
}
