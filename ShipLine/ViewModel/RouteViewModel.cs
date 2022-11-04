using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using ShipLine.Models;
using ShipLine.Repository;
using System.Collections.Generic;

namespace ShipLine.ViewModel
{
    public class RouteViewModel
    {
        public Guid RouteId { get; set; }
        public Guid SourcePortId { get; set; }
        public Guid DestinationPortId { get; set; }
        public string Name { get; set; } = null!;
        public string SourcePortName { get; set; }
        public string DestinationPortName { get; set; }

        public RouteViewModel(RouteModel model, PortRepository repository)
        {
            this.RouteId = model.RouteId;
            this.SourcePortId = model.SourcePortId;
            this.DestinationPortId = model.DestinationPortId;
            this.Name = model.Name;
            var sourcePort = repository.GetPortById(model.SourcePortId);
            this.SourcePortName = sourcePort.Name;
            var destinationPort = repository.GetPortById(model.DestinationPortId);
            this.DestinationPortName = destinationPort.Name;          
        }
    }
}
