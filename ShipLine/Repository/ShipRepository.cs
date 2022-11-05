using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Models.DBObjects;

namespace ShipLine.Repository
{
    public class ShipRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public ShipRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public ShipRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }
        
        private ShipModel MapDBObjectToModel(Ship dbObject)
        {
            var model = new ShipModel();
            if(dbObject != null)
            {
                model.ShipId = dbObject.ShipId;
                model.Name = dbObject.Name;
                model.Capacity = dbObject.Capacity;
            }
            return model;
        }
        private Ship MapModelToDBObject (ShipModel model)
        {
            var dbObject = new Ship();
            if(dbObject != null)
            {
                dbObject.ShipId = model.ShipId;
                dbObject.Name = model.Name;
                dbObject.Capacity = model.Capacity;
            }
            return dbObject;
        }

        public List<ShipModel> GetAllShips()
        {
            var list = new List<ShipModel>();
            foreach(var dbObject in _DBContext.Ships)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }
        public ShipModel GetShipById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Ships.FirstOrDefault(x => x.ShipId == id));
        }
        public void InsertShip(ShipModel model)
        {
            model.ShipId = Guid.NewGuid();
            _DBContext.Ships.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateShip(ShipModel model)
        {
            var dbObject = _DBContext.Ships.FirstOrDefault(x=> x.ShipId == model.ShipId);
            if(dbObject != null)
            {
                dbObject.ShipId = model.ShipId;
                dbObject.Name = model.Name;
                dbObject.Capacity = model.Capacity;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteShip(Guid id)
        {
            var dbObject = _DBContext.Ships.FirstOrDefault(x=> x.ShipId == id);
            if(dbObject != null)
            {
                var voyages = _DBContext.Voyages.Where(x => x.ShipId == dbObject.ShipId);
                foreach (var voyage in voyages)
                {
                    _DBContext.Voyages.Remove(voyage);
                }
                _DBContext.Ships.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
    }
}
