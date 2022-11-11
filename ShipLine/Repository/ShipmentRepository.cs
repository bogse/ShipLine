using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Models.DBObjects;

namespace ShipLine.Repository
{
    public class ShipmentRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public ShipmentRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public ShipmentRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private ShipmentModel MapDBObjectToModel(Shipment dbObject)
        {
            var model = new ShipmentModel();
            if(dbObject != null)
            {
                model.ShipmentId = dbObject.ShipmentId;
                model.CustomerId = dbObject.CustomerId;
                model.CargoContents = dbObject.CargoContents;
                model.QuantityTeq = dbObject.QuantityTeq;
                model.ShipRequestDate = dbObject.ShipRequestDate;
                model.NeedByDate = dbObject.NeedByDate;
                model.Status = dbObject.Status;
                model.DestinationPortId = dbObject.DestinationPortId;
                model.SourcePortId = dbObject.SourcePortId;
                model.ShipmentNumber = dbObject.ShipmentNumber;
            }
            return model;
        }
        private Shipment MapModelToDBObject(ShipmentModel model)
        {
            var dbObject = new Shipment();
            if(dbObject != null)
            {
                dbObject.ShipmentId = model.ShipmentId;
                dbObject.CustomerId = model.CustomerId;
                dbObject.CargoContents = model.CargoContents;
                dbObject.QuantityTeq = model.QuantityTeq;
                dbObject.ShipRequestDate = model.ShipRequestDate;
                dbObject.NeedByDate = model.NeedByDate;
                dbObject.Status = model.Status;
                dbObject.DestinationPortId = model.DestinationPortId;
                dbObject.SourcePortId = model.SourcePortId;
                dbObject.ShipmentNumber = model.ShipmentNumber;
            }
            return dbObject;
        }
        public List<ShipmentModel> GetAllShipments()
        {
            var list = new List<ShipmentModel>();
            foreach(var dbObject in _DBContext.Shipments)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }
        public ShipmentModel GetShipmentById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Shipments.FirstOrDefault(x => x.ShipmentId == id));
        }
        public void InsertShipment(ShipmentModel model)
        {
            model.ShipmentId = Guid.NewGuid();
            _DBContext.Shipments.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateShipment(ShipmentModel model)
        {
            var dbObject = _DBContext.Shipments.FirstOrDefault(x=> x.ShipmentId == model.ShipmentId);
            if(dbObject != null)
            {
                dbObject.ShipmentId = model.ShipmentId;
                dbObject.CustomerId = model.CustomerId;
                dbObject.CargoContents = model.CargoContents;
                dbObject.QuantityTeq = model.QuantityTeq;
                dbObject.ShipRequestDate = model.ShipRequestDate;
                dbObject.NeedByDate = model.NeedByDate;
                dbObject.Status = model.Status;
                dbObject.DestinationPortId = model.DestinationPortId;
                dbObject.SourcePortId = model.SourcePortId;
                dbObject.ShipmentNumber = model.ShipmentNumber;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteShipment(Guid id)
        {
            var dbObject = _DBContext.Shipments.FirstOrDefault(x=> x.ShipmentId == id);

            if(dbObject != null)
            {
                var voyageShipments = _DBContext.VoyageShipments.Where(x => x.ShipmentId == dbObject.ShipmentId);
                foreach (var voyageShipment in voyageShipments)
                {
                    _DBContext.VoyageShipments.Remove(voyageShipment);
                }
                _DBContext.Shipments.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
        public int GetTotalQuantityPerVoyage(Guid id)
        {
            var voyageShipments = _DBContext.VoyageShipments.Where(x => x.VoyageId == id);

            var shipmentList = new List<ShipmentModel>();
            var model = new ShipmentModel();
            foreach (var voyageShipment in voyageShipments)
            {
                shipmentList.Add(MapDBObjectToModel(_DBContext.Shipments.FirstOrDefault(x => x.ShipmentId == voyageShipment.ShipmentId)));
            }

            int sum = 0;
            foreach(var shipment in shipmentList)
            {
                sum += shipment.QuantityTeq;
            }

            return sum;
        }
        public List<ShipmentModel> GetShipmentsByRoute(Guid id)
        {
            var routes = _DBContext.Routes.Where(x => x.RouteId == id);

            var shipmentList = new List<ShipmentModel>();
            foreach (var route in routes)
            {
                shipmentList.Add(MapDBObjectToModel(_DBContext.Shipments
                            .FirstOrDefault(x => x.DestinationPortId == route.DestinationPortId && x.SourcePortId == route.DestinationPortId)));
            }
            //shipmentList = shipmentList.Where(x => x.NeedByDate > DateTime.Now).ToList(); // asa se fac query-uri profesor

            return shipmentList;
        }
    }
}
