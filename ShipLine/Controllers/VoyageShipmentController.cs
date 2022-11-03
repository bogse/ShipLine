using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;

namespace ShipLine.Controllers
{
    public class VoyageShipmentController : Controller
    {
        private VoyageShipmentRepository _voyageShipmentRepository;
        public VoyageShipmentController(ApplicationDbContext dbContext)
        {
            _voyageShipmentRepository = new VoyageShipmentRepository(dbContext);
        }
        // GET: VoyageShipmentController
        public ActionResult Index()
        {
            var list = _voyageShipmentRepository.GetAllVoyageShipments();
            return View("Index", list);
        }

        // GET: VoyageShipmentController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _voyageShipmentRepository.GetVoyageShipmentById(id);
            return View("DetailsVoyageShipment", model);
        }

        // GET: VoyageShipmentController/Create
        public ActionResult Create()
        {
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
            return View("EditVoyageShipment", model);
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
            return View("DeleteVoyageShipment", model);
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
