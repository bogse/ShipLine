using ShipLine.Models;
using ShipLine.Models.DBObjects;
using ShipLine.Repository;

namespace ShipLine.ViewModel
{
    public class VoyageViewModel
    {
        public Guid VoyageId { get; set; }
        public Guid ShipId { get; set; }
        public Guid RouteId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int VoyageQuantity { get; set; }
        public int CostPerTeq { get; set; }
        public int VoyageNumber { get; set; }
        public string ShipName { get; set; }
        public string RouteName { get; set; }

        public VoyageViewModel(VoyageModel model, ShipRepository shipRepository, RouteRepository routeRepository)
        {
            this.VoyageId = model.VoyageId;
            this.ShipId = model.ShipId;
            this.RouteId = model.RouteId;
            this.StartDate = model.StartDate;
            this.EndDate = model.EndDate;
            this.VoyageQuantity = model.VoyageQuantity;
            this.CostPerTeq = model.CostPerTeq;
            this.VoyageNumber = model.VoyageNumber;
            var ship = shipRepository.GetShipById(model.ShipId);
            this.ShipName = ship.Name;
            var route = routeRepository.GetRouteById(model.RouteId);
            this.RouteName = route.Name;
        }
    }
}
