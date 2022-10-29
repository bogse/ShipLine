using System;
using System.Collections.Generic;

namespace ShipLine.Models.DBObjects
{
    public partial class Port
    {
        public Port()
        {
            RouteDestinationPorts = new HashSet<Route>();
            RouteSourcePorts = new HashSet<Route>();
        }

        public Guid PortId { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;

        public virtual ICollection<Route> RouteDestinationPorts { get; set; }
        public virtual ICollection<Route> RouteSourcePorts { get; set; }
    }
}
