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

        public VoyageShipmentViewModel(VoyageShipmentModel model, ShipmentRepository shipmentRepository, VoyageRepository voyageRepository)
        {
            this.VoyageShipmentId = model.VoyageShipmentId;
            this.ShipmentId = model.ShipmentId;
            this.VoyageId = model.VoyageId;
            var shipment = shipmentRepository.GetShipmentById(model.ShipmentId);
            this.ShipmentNumber = shipment.ShipmentNumber;
            var voyage = voyageRepository.GetVoyageById(model.VoyageId);
            this.VoyageNumber = voyage.VoyageNumber;
        }
    }
}
