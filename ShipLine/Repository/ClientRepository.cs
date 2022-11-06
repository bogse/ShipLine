using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Models.DBObjects;

namespace ShipLine.Repository
{
    public class ClientRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public ClientRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public ClientRepository(ApplicationDbContext dbContext)
        {
            _DBContext = dbContext;
        }

        private ClientModel MapDBObjectToModel(Client dbObject)
        {
            var model = new ClientModel();
            if (dbObject != null)
            {
                model.ClientId = dbObject.ClientId;
                model.ClientName = dbObject.ClientName;
                model.Email = dbObject.Email;
                model.Phone = dbObject.Phone;
            }
            return model;
        }
        private Client MapModelToDBObject(ClientModel model)
        {
            var dbObject = new Client();
            if(dbObject != null)
            {
                dbObject.ClientId = model.ClientId;
                dbObject.ClientName = model.ClientName;
                dbObject.Email = model.Email;
                dbObject.Phone = model.Phone;
            }
            return dbObject;
        }
        public List<ClientModel> GetAllClients()
        {
            var list = new List<ClientModel>();
            foreach (var dbObject in _DBContext.Clients)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }
        public ClientModel GetClientById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Clients.FirstOrDefault(x => x.ClientId == id));
        }
        public void InsertClient(ClientModel model)
        {
            model.ClientId = Guid.NewGuid();
            _DBContext.Clients.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateClient(ClientModel model)
        {
            var dbObject = _DBContext.Clients.FirstOrDefault(x => x.ClientId == model.ClientId);
            if (dbObject != null)
            {
                dbObject.ClientId = model.ClientId;
                dbObject.ClientName = model.ClientName;
                dbObject.Email = model.Email;
                dbObject.Phone = model.Phone;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteClient(Guid id)
        {
            var dbObject = _DBContext.Clients.FirstOrDefault(x => x.ClientId == id);
            if (dbObject != null)
            {
                var shipments = _DBContext.Shipments.Where(x => x.CustomerId == dbObject.ClientId);
                foreach (var shipment in shipments)
                {
                    _DBContext.Shipments.Remove(shipment);
                }
                _DBContext.Clients.Remove(dbObject);
                _DBContext.SaveChanges();
            }
        }
    }
}
