using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;
using ShipLine.ViewModel;

namespace ShipLine.Controllers
{
    public class VoyageController : Controller
    {
        private VoyageRepository _voyageRepository;
        private ShipRepository _shipRepository;
        private RouteRepository _routeRepository;
        private ShipmentRepository _shipmentRepository;
        private VoyageShipmentRepository _voyageShipmentRepository;

        public VoyageController(ApplicationDbContext dbContext)
        {
            _voyageRepository = new VoyageRepository(dbContext);
            _shipRepository = new ShipRepository(dbContext);
            _routeRepository = new RouteRepository(dbContext);
            _shipmentRepository = new ShipmentRepository(dbContext);
            _voyageShipmentRepository = new VoyageShipmentRepository(dbContext);
        }
        // GET: VoyageController
        public ActionResult Index()
        {
            var list = _voyageRepository.GetAllVoyages();
            var viewModelList = new List<VoyageViewModel>();
            foreach(var voyage in list)
            {
                viewModelList.Add(new VoyageViewModel(voyage, _shipRepository, _routeRepository, _shipmentRepository));
            }

            return View("Index", viewModelList);
        }

        // GET: VoyageController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _voyageRepository.GetVoyageById(id);
            var viewModel = new VoyageViewModel(model, _shipRepository, _routeRepository, _shipmentRepository);

            return View("DetailsVoyage", viewModel);
        }

        // GET: VoyageController/Create
        public ActionResult Create()
        {
            var ships = _shipRepository.GetAllShips();
            var shipList = ships.Select(x => new SelectListItem(x.Name, x.ShipId.ToString()));
            ViewBag.ShipList = shipList;

            var routes = _routeRepository.GetAllRoutes();
            var routeList = routes.Select(x => new SelectListItem(x.Name, x.RouteId.ToString()));
            ViewBag.RouteList = routeList;

            return View("CreateVoyage");
        }

        // POST: VoyageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new VoyageModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _voyageRepository.InsertVoyage(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateVoyage");
            }
        }

        // GET: VoyageController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _voyageRepository.GetVoyageById(id);

            var ships = _shipRepository.GetAllShips();
            var shipList = ships.Select(x=> new SelectListItem(x.Name, x.ShipId.ToString()));
            ViewBag.ShipList = shipList;

            var routes = _routeRepository.GetAllRoutes();
            var routeList = routes.Select(x => new SelectListItem(x.Name, x.RouteId.ToString()));
            ViewBag.RouteList = routeList;

            return View("EditVoyage", model);
        }

        // POST: VoyageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new VoyageModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _voyageRepository.UpdateVoyage(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: VoyageController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _voyageRepository.GetVoyageById(id);
            var viewModel = new VoyageViewModel(model, _shipRepository, _routeRepository, _shipmentRepository);

            return View("DeleteVoyage", viewModel);
        }

        // POST: VoyageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _voyageRepository.DeleteVoyage(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
        public ActionResult AddShipment(Guid id)
        {
            var model = _voyageRepository.GetVoyageById(id);

            var shipments = _shipmentRepository.GetAllShipments();
            var shipmentList = shipments.Select(x => new SelectListItem(x.ShipmentNumber.ToString(), x.ShipmentId.ToString()));
            ViewBag.ShipmentList = shipmentList;

            return View("AddShipment");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddShipment(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new VoyageShipmentModel();
                model.VoyageId = id;
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
                return View("AddShipment");
            }
        }
    }
}
