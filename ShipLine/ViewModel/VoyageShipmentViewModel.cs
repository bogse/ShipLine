using ShipLine.Models;
using ShipLine.Repository;

namespace ShipLine.ViewModel
{
    public class VoyageShipmentViewModel
    {
        public Guid VoyageShipmentId { get; set; }
        public Guid ShipmentId { get; set; }
        public Guid VoyageId { get; set; }
        public int ShipmentNumber { get; set; }
        public int VoyageNumber { get; set; }
        public string DestinationPort { get; set; }
        public string SourcePort { get; set; }
        public string ShipRoute { get; set; }
        public string CargoContents { get; set; }
        public int CostPerTeq { get; set; }
        public int QuantityTeq { get; set; }
        public int VoyageQuantity { get; set; }
        public int TotalCost => VoyageQuantity * CostPerTeq;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime NeedByDate { get; set; }


        public VoyageShipmentViewModel(VoyageShipmentModel model, ShipmentRepository shipmentRepository, 
                VoyageRepository voyageRepository, PortRepository portRepository, RouteRepository routeRepository)
        {
            this.VoyageShipmentId = model.VoyageShipmentId;
            this.ShipmentId = model.ShipmentId;
            this.VoyageId = model.VoyageId;
            var shipment = shipmentRepository.GetShipmentById(model.ShipmentId);
            this.ShipmentNumber = shipment.ShipmentNumber;
            var voyage = voyageRepository.GetVoyageById(model.VoyageId);
            this.VoyageNumber = voyage.VoyageNumber;
            var destinationPort = portRepository.GetPortById(shipment.DestinationPortId);
            this.DestinationPort = destinationPort.Name;
            var sourcePort = portRepository.GetPortById(shipment.SourcePortId);
            this.SourcePort = sourcePort.Name;
            var route = routeRepository.GetRouteById(voyage.RouteId);
            this.ShipRoute = route.Name;
            this.CargoContents = shipment.CargoContents;
            this.CostPerTeq = voyage.CostPerTeq;
            this.QuantityTeq = shipment.QuantityTeq;
            this.VoyageQuantity = voyage.VoyageQuantity;
            this.StartDate = voyage.StartDate;
            this.EndDate = voyage.EndDate;
            this.NeedByDate = shipment.NeedByDate;
        }
    }
}
