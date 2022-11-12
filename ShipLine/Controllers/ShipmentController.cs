using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;
using ShipLine.ViewModel;

namespace ShipLine.Controllers
{
    public class ShipmentController : Controller
    {
        private ShipmentRepository _shipmentRepository;
        private ClientRepository _clientRepository;
        private PortRepository _portRepository;
        private VoyageShipmentRepository _voyageShipmentRepository;
        private VoyageRepository _voyageRepository;
        private RouteRepository _routeRepository;
        public ShipmentController(ApplicationDbContext dbContext)
        {
            _shipmentRepository = new ShipmentRepository(dbContext);
            _clientRepository = new ClientRepository(dbContext);
            _portRepository = new PortRepository(dbContext);
            _voyageShipmentRepository = new VoyageShipmentRepository(dbContext);
            _voyageRepository = new VoyageRepository(dbContext);
            _routeRepository = new RouteRepository(dbContext);
        }
        // GET: ShipmentController
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            ViewData["StatusSortParam"] = String.IsNullOrEmpty(sortOrder) ? "StatusDesc" : "";
            ViewData["NeedByDateSortParam"] = sortOrder == "NeedByDate" ? "NeedByDateDesc" : "NeedByDate";
            ViewData["ShipRequestDateSortParam"] = sortOrder == "ShipRequestDate" ? "ShipRequestDateDesc" : "ShipRequestDate";
            ViewData["ShipmentNumberSortParam"] = sortOrder == "ShipmentNumber" ? "ShipmentNumberDesc" : "ShipmentNumber";
            ViewData["QuantitySortParam"] = sortOrder == "Quantity" ? "QuantityDesc" : "Quantity";
            ViewData["CurrentFilterSortParam"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var list = _shipmentRepository.GetAllShipments();
            var viewModelList = new List<ShipmentViewModel>();
            foreach (var shipment in list)
            {
                viewModelList.Add(new ShipmentViewModel(shipment, _clientRepository, _portRepository));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModelList = viewModelList.Where(s => s.CustomerName!.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "StatusDesc":
                    viewModelList = viewModelList.OrderByDescending(x => x.CustomerName).ToList();
                    break;
                case "NeedByDate":
                    viewModelList = viewModelList.OrderBy(x => x.NeedByDate).ToList();
                    break;
                case "NeedByDateDesc":
                    viewModelList = viewModelList.OrderByDescending(x => x.NeedByDate).ToList();
                    break;
                case "ShipRequestDate":
                    viewModelList = viewModelList.OrderBy(x => x.ShipRequestDate).ToList();
                    break;
                case "ShipRequestDateDesc":
                    viewModelList = viewModelList.OrderByDescending(x => x.ShipRequestDate).ToList();
                    break;
                case "ShipmentNumber":
                    viewModelList = viewModelList.OrderBy(x => x.ShipmentNumber).ToList();
                    break;
                case "ShipmentNumberDesc":
                    viewModelList = viewModelList.OrderByDescending(x => x.ShipmentNumber).ToList();
                    break;
                case "Quantity":
                    viewModelList = viewModelList.OrderBy(x => x.QuantityTeq).ToList();
                    break;
                case "QuantityDesc":
                    viewModelList = viewModelList.OrderByDescending(x => x.QuantityTeq).ToList();
                    break;
                default:
                    viewModelList = viewModelList.OrderBy(x => x.CustomerName).ToList();
                    break;
            }

            int pageSize = 3;

            return View("Index", viewModelList);
        }

        // GET: ShipmentController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _shipmentRepository.GetShipmentById(id);
            var viewModel = new ShipmentViewModel(model, _clientRepository, _portRepository);

            var shipmentVoyages = _voyageShipmentRepository.GetAllVoyageShipments().Where(x => x.ShipmentId == model.ShipmentId);
            
            var list = new List<VoyageModel>();
            foreach (var voyage in shipmentVoyages)
            {
                list.Add(_voyageRepository.GetVoyageById(voyage.VoyageId));
            }
            ViewData["Voyages"] = list;

            return View("DetailsShipment", viewModel);
        }

        // GET: ShipmentController/Create
        public ActionResult Create()
        {
            var clients = _clientRepository.GetAllClients();
            var clientList = clients.Select(x => new SelectListItem(x.ClientName, x.ClientId.ToString()));
            ViewBag.ClientList = clientList;

            var destinationPorts = _portRepository.GetAllPorts();
            var destinationPortList = destinationPorts.Select(x => new SelectListItem(x.Name, x.PortId.ToString()));
            ViewBag.DestinationPortList = destinationPortList;

            var sourcePorts = _portRepository.GetAllPorts();
            var sourcePortList = sourcePorts.Select(x => new SelectListItem(x.Name, x.PortId.ToString()));
            ViewBag.SourcePortList = sourcePortList;

            return View("CreateShipment");
        }

        // POST: ShipmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new ShipmentModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _shipmentRepository.InsertShipment(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateShipment");
            }
        }

        // GET: ShipmentController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _shipmentRepository.GetShipmentById(id);

            var clients = _clientRepository.GetAllClients();
            var clientList = clients.Select(x => new SelectListItem(x.ClientName, x.ClientId.ToString()));
            ViewBag.ClientList = clientList;

            var destinationPorts = _portRepository.GetAllPorts();
            var destinationPortList = destinationPorts.Select(x => new SelectListItem(x.Name, x.PortId.ToString()));
            ViewBag.DestinationPortList = destinationPortList;

            var sourcePorts = _portRepository.GetAllPorts();
            var sourcePortList = sourcePorts.Select(x => new SelectListItem(x.Name, x.PortId.ToString()));
            ViewBag.SourcePortList = sourcePortList;

            return View("EditShipment", model);
        }

        // POST: ShipmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new ShipmentModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _shipmentRepository.UpdateShipment(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: ShipmentController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _shipmentRepository.GetShipmentById(id);
            var viewModel = new ShipmentViewModel(model, _clientRepository, _portRepository);

            return View("DeleteShipment", viewModel);
        }

        // POST: ShipmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _shipmentRepository.DeleteShipment(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
        public ActionResult AddToVoyage(Guid id)
        {
            var shipment = _shipmentRepository.GetShipmentById(id);

            var route = _routeRepository.GetRouteById(shipment.SourcePortId, shipment.DestinationPortId);
            var voyages = _voyageRepository.GetAllVoyages().Where(x => x.RouteId == route.RouteId && x.StartDate > DateTime.Now && x.EndDate <= shipment.NeedByDate);
            var voyageList = voyages.Select(x => new SelectListItem(x.VoyageNumber.ToString(), x.VoyageId.ToString()));
            ViewBag.VoyageList = voyageList;

            return View("AddToVoyage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToVoyage(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new VoyageShipmentModel();
                model.ShipmentId = id;
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _voyageShipmentRepository.InsertVoyageShipment(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("AddToVoyage");
            }
        }
    }
}
