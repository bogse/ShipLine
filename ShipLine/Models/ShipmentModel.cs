using System.ComponentModel.DataAnnotations;

namespace ShipLine.Models
{
    public class ShipmentModel
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
    }
}
