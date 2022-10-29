using System;
using System.Collections.Generic;

namespace ShipLine.Models.DBObjects
{
    public partial class Ship
    {
        public Ship()
        {
            Voyages = new HashSet<Voyage>();
        }

        public Guid ShipId { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }

        public virtual ICollection<Voyage> Voyages { get; set; }
    }
}
