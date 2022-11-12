using ShipLine.Models;
using ShipLine.Models.DBObjects;
using ShipLine.Repository;
using System.ComponentModel.DataAnnotations;

namespace ShipLine.ViewModel
{
    public class VoyageViewModel
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
        public string ShipName { get; set; }
        public string RouteName { get; set; }

        public VoyageViewModel(VoyageModel model, ShipRepository shipRepository, RouteRepository routeRepository, ShipmentRepository shipmentRepository)
        {
            this.VoyageId = model.VoyageId;
            this.ShipId = model.ShipId;
            this.RouteId = model.RouteId;
            this.StartDate = model.StartDate;
            this.EndDate = model.EndDate;
            this.VoyageQuantity = shipmentRepository.GetTotalQuantityPerVoyage(model.VoyageId);
            this.CostPerTeq = model.CostPerTeq;
            this.VoyageNumber = model.VoyageNumber;
            var ship = shipRepository.GetShipById(model.ShipId);
            this.ShipName = ship.Name;
            var route = routeRepository.GetRouteById(model.RouteId);
            this.RouteName = route.Name;
        }
    }
}
