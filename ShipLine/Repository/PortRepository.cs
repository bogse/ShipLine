using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Models.DBObjects;

namespace ShipLine.Repository
{
    public class PortRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public PortRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public PortRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private PortModel MapDBObjectToModel(Port dbObject)
        {
            var model = new PortModel();
            if(dbObject != null)
            {
                model.PortId = dbObject.PortId;
                model.Name = dbObject.Name;
                model.City = dbObject.City;
            }
            return model;
        }
        private Port MapModelToDBObject(PortModel model)
        {
            var dbObject = new Port();
            if(dbObject != null)
            {
                dbObject.PortId = model.PortId;
                dbObject.Name = model.Name;
                dbObject.City = model.City;
            }
            return dbObject;
        }

        public List<PortModel> GetAllPorts()
        {
            var list = new List<PortModel>();
            foreach (var dbObject in _DBContext.Ports)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }
        public PortModel GetPortById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Ports.FirstOrDefault(x => x.PortId == id));
        }

        public void InsertPort(PortModel model)
        {
            model.PortId = Guid.NewGuid();
            _DBContext.Ports.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }

        public void UpdatePort(PortModel model)
        {
            var dbObject = _DBContext.Ports.FirstOrDefault(x => x.PortId == model.PortId);
            if(dbObject != null)
            {
                dbObject.PortId = model.PortId;
                dbObject.Name = model.Name;
                dbObject.City = model.City;
                _DBContext.SaveChanges();
            }
        }

        public void DeletePort(Guid id)
        {
            var dbObject = _DBContext.Ports.FirstOrDefault(x => x.PortId == id);
            if(dbObject != null)
            {
                var routes = _DBContext.Routes.Where(x => x.SourcePortId == dbObject.PortId || x.DestinationPortId == dbObject.PortId);
                foreach (var route in routes)
                {
                    var voyages = _DBContext.Voyages.Where(x => x.RouteId == route.RouteId);
                    foreach (var voyage in voyages)
                    {
                        _DBContext.Voyages.Remove(voyage);
                    }
                    _DBContext.Routes.Remove(route);
                }

                var shipments = _DBContext.Shipments.Where(x => x.SourcePortId == dbObject.PortId || x.DestinationPortId == dbObject.PortId);
                foreach (var shipment in shipments)
                {
                    _DBContext.Shipments.Remove(shipment);
                }

                _DBContext.Ports.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
    }
}
