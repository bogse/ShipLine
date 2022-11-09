using Microsoft.EntityFrameworkCore;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Models.DBObjects;
using System.Collections.Generic;

namespace ShipLine.Repository
{
    public class VoyageShipmentRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public VoyageShipmentRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public VoyageShipmentRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }
        private VoyageShipmentModel MapDBObjectToModel(VoyageShipment dbObject)
        {
            var model = new VoyageShipmentModel();
            if(dbObject != null)
            {
                model.VoyageShipmentId = dbObject.VoyageShipmentId;
                model.ShipmentId = dbObject.ShipmentId;
                model.VoyageId = dbObject.VoyageId;
            }
            return model;
        }
        private VoyageShipment MapModelToDBObject(VoyageShipmentModel model)
        {
            var dbObject = new VoyageShipment();
            if(dbObject != null)
            {
                dbObject.VoyageShipmentId = model.VoyageShipmentId;
                dbObject.ShipmentId = model.ShipmentId;
                dbObject.VoyageId = model.VoyageId;
            }
            return dbObject;
        }
        public List<VoyageShipmentModel> GetAllVoyageShipments()
        {
            var list = new List<VoyageShipmentModel>();
            foreach(var dbObject in _DBContext.VoyageShipments)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }
        public VoyageShipmentModel GetVoyageShipmentById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.VoyageShipments.FirstOrDefault(x=> x.VoyageShipmentId == id));
        }
        public void InsertVoyageShipment(VoyageShipmentModel model)
        {
            model.VoyageShipmentId = Guid.NewGuid();
            _DBContext.VoyageShipments.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateVoyageShipment(VoyageShipmentModel model)
        {
            var dbObject = _DBContext.VoyageShipments.FirstOrDefault(x => x.VoyageShipmentId == model.VoyageShipmentId);
            if(dbObject != null)
            {
                dbObject.VoyageShipmentId = model.VoyageShipmentId;
                dbObject.ShipmentId = model.ShipmentId;
                dbObject.VoyageId = model.VoyageId;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteVoyageShipment(Guid id)
        {
            var dbObject = _DBContext.VoyageShipments.FirstOrDefault(x=> x.VoyageShipmentId == id);
            if (dbObject != null)
            {
                _DBContext.VoyageShipments.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
    }
}
