using Microsoft.EntityFrameworkCore;
using ShipLine.CustomValidator;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Models.DBObjects;

namespace ShipLine.Repository
{
    public class VoyageRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public VoyageRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public VoyageRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private VoyageModel MapDBObjectToModel(Voyage dbObject)
        {
            var model = new VoyageModel();
            if(dbObject != null)
            {
                model.VoyageId = dbObject.VoyageId;
                model.ShipId = dbObject.ShipId;
                model.RouteId = dbObject.RouteId;
                model.StartDate = dbObject.StartDate;
                model.EndDate = dbObject.EndDate;
                model.VoyageQuantity = dbObject.VoyageQuantity;
                model.CostPerTeq = dbObject.CostPerTeq;
                model.VoyageNumber = dbObject.VoyageNumber;
            }
            return model;
        }
        private Voyage MapModelToDBObject(VoyageModel model)
        {
            var dbObject = new Voyage();
            if(dbObject != null)
            {
                dbObject.VoyageId = model.VoyageId;
                dbObject.ShipId = model.ShipId;
                dbObject.RouteId = model.RouteId;
                dbObject.StartDate = model.StartDate;
                dbObject.EndDate = model.EndDate;
                dbObject.VoyageQuantity = model.VoyageQuantity;
                dbObject.CostPerTeq = model.CostPerTeq;
                dbObject.VoyageNumber = model.VoyageNumber;
            }
            return dbObject;
        }
        public List<VoyageModel> GetAllVoyages()
        {
            var list = new List<VoyageModel>();
            foreach(var dbObject in _DBContext.Voyages) 
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }
        public VoyageModel GetVoyageById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Voyages.FirstOrDefault(x => x.VoyageId == id));
        }
        public void InsertVoyage(VoyageModel model)
        {
            model.VoyageId = Guid.NewGuid();
            _DBContext.Voyages.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateVoyage(VoyageModel model)
        {
            var dbObject = _DBContext.Voyages.FirstOrDefault(x=> x.VoyageId == model.VoyageId);
            if (dbObject != null)
            {
                dbObject.VoyageId = model.VoyageId;
                dbObject.ShipId = model.ShipId;
                dbObject.RouteId = model.RouteId;
                dbObject.StartDate = model.StartDate;
                dbObject.EndDate = model.EndDate;
                dbObject.VoyageQuantity = model.VoyageQuantity;
                dbObject.CostPerTeq = model.CostPerTeq;
                dbObject.VoyageNumber = model.VoyageNumber;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteVoyage(Guid id)
        {
            var dbObject = _DBContext.Voyages.FirstOrDefault(x => x.VoyageId == id);
            if(dbObject != null)
            {
                var voyageShipments = _DBContext.VoyageShipments.Where(x => x.VoyageId == dbObject.VoyageId);
                foreach(var voyageShipment in voyageShipments)
                {
                    _DBContext.VoyageShipments.Remove(voyageShipment);
                }
                _DBContext.Voyages.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
    }
}
