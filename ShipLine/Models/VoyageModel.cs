using System.ComponentModel.DataAnnotations;

namespace ShipLine.Models
{
    public class VoyageModel
    {
        public Guid VoyageId { get; set; }
        public Guid ShipId { get; set; }
        public Guid RouteId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int VoyageQuantity { get; set; }
        public int CostPerTeq { get; set; }
        public int VoyageNumber { get; set; }
    }
}
