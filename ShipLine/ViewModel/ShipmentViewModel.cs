using ShipLine.Models;
using ShipLine.Repository;
using System.ComponentModel.DataAnnotations;

namespace ShipLine.ViewModel
{
    public class ShipmentViewModel
    {
        public Guid ShipmentId { get; set; }
        public Guid CustomerId { get; set; }
        public string CargoContents { get; set; } = null!;
        public int QuantityTeq { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ShipRequestDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime NeedByDate { get; set; }
        public string Status { get; set; } = null!;
        public Guid DestinationPortId { get; set; }
        public Guid SourcePortId { get; set; }
        public int ShipmentNumber { get; set; }
        public string CustomerName { get; set; }
        public string DestinationPort { get; set; }
        public string SourcePort { get; set; }

        public ShipmentViewModel(ShipmentModel model, ClientRepository clientRepository, PortRepository portRepository)
        {
            this.ShipmentId = model.ShipmentId;
            this.CustomerId = model.CustomerId;
            this.CargoContents = model.CargoContents;
            this.QuantityTeq = model.QuantityTeq;
            this.ShipRequestDate = model.ShipRequestDate;
            this.NeedByDate = model.NeedByDate;
            this.Status = model.Status;
            this.DestinationPortId = model.DestinationPortId;
            this.SourcePortId = model.SourcePortId;
            this.ShipmentNumber = model.ShipmentNumber;
            var client = clientRepository.GetClientById(model.CustomerId);
            this.CustomerName = client.ClientName;
            var destinationPort = portRepository.GetPortById(model.DestinationPortId);
            this.DestinationPort = destinationPort.Name;
            var sourcePort = portRepository.GetPortById(model.SourcePortId);
            this.SourcePort = sourcePort.Name;
        }
    }
}
