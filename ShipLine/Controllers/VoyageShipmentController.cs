using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Models.DBObjects;
using ShipLine.Repository;
using ShipLine.ViewModel;
using System.Diagnostics;
using System.Xml.Schema;

namespace ShipLine.Controllers
{
    public class VoyageShipmentController : Controller
    {
        private VoyageShipmentRepository _voyageShipmentRepository;
        private ShipmentRepository _shipmentRepository;
        private VoyageRepository _voyageRepository;
        private RouteRepository _routeRepository;
        private PortRepository _portRepository;
        public VoyageShipmentController(ApplicationDbContext dbContext)
        {
            _voyageShipmentRepository = new VoyageShipmentRepository(dbContext);
            _shipmentRepository = new ShipmentRepository(dbContext);
            _voyageRepository = new VoyageRepository(dbContext);
            _routeRepository = new RouteRepository(dbContext);
            _portRepository = new PortRepository(dbContext);
        }
        // GET: VoyageShipmentController
        public ActionResult Index()
        {
            var list = _voyageShipmentRepository.GetAllVoyageShipments();
            var viewModelList = new List<VoyageShipmentViewModel>();

            foreach (var voyageShipment in list)
            {
                viewModelList.Add(new VoyageShipmentViewModel(voyageShipment, _shipmentRepository, _voyageRepository, _portRepository, _routeRepository));
            }

            return View("Index", viewModelList);
        }

        // GET: VoyageShipmentController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _voyageShipmentRepository.GetVoyageShipmentById(id);
            var viewModel = new VoyageShipmentViewModel(model, _shipmentRepository, _voyageRepository, _portRepository, _routeRepository);

            return View("DetailsVoyageShipment", viewModel);
        }

        // GET: VoyageShipmentController/Create
        public ActionResult Create()
        {
            var voyages = _voyageRepository.GetAllVoyages();
            var voyageList = voyages.Select(x => new SelectListItem(x.VoyageNumber.ToString(), x.VoyageId.ToString()));
            ViewBag.VoyageList = voyageList;

            var shipments = _shipmentRepository.GetAllShipments();
            var shipmentList = shipments.Select(x => new SelectListItem(x.ShipmentNumber.ToString(), x.ShipmentId.ToString()));
            ViewBag.ShipmentList = shipmentList;

            return View("CreateVoyageShipment");
        }

        // POST: VoyageShipmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new VoyageShipmentModel();               
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
                return View("CreateVoyageShipment");
            }
        }

        // GET: VoyageShipmentController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _voyageShipmentRepository.GetVoyageShipmentById(id);
            var viewModel = new VoyageShipmentViewModel(model, _shipmentRepository, _voyageRepository, _portRepository, _routeRepository);

            var shipments = _shipmentRepository.GetAllShipments();
            var shipmentList = shipments.Select(x => new SelectListItem(x.ShipmentNumber.ToString(), x.ShipmentId.ToString()));
            ViewBag.ShipmentList = shipmentList;

            var voyages = _voyageRepository.GetAllVoyages();
            var voyageList = voyages.Select(x => new SelectListItem(x.VoyageNumber.ToString(), x.VoyageId.ToString()));
            ViewBag.VoyageList = voyageList;

            return View("EditVoyageShipment", viewModel);
        }

        // POST: VoyageShipmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new VoyageShipmentModel();
                
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _voyageShipmentRepository.UpdateVoyageShipment(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: VoyageShipmentController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _voyageShipmentRepository.GetVoyageShipmentById(id);
            var viewModel = new VoyageShipmentViewModel(model, _shipmentRepository, _voyageRepository, _portRepository, _routeRepository);

            return View("DeleteVoyageShipment", viewModel);
        }

        // POST: VoyageShipmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _voyageShipmentRepository.DeleteVoyageShipment(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
