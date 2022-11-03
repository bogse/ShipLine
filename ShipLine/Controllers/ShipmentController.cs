using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;

namespace ShipLine.Controllers
{
    public class ShipmentController : Controller
    {
        private ShipmentRepository _shipmentRepository;
        public ShipmentController(ApplicationDbContext dbContext)
        {
            _shipmentRepository = new ShipmentRepository(dbContext);
        }
        // GET: ShipmentController
        public ActionResult Index()
        {
            var list = _shipmentRepository.GetAllShipments();
            return View("Index", list);
        }

        // GET: ShipmentController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _shipmentRepository.GetShipmentById(id);
            return View("DetailsShipment", model);
        }

        // GET: ShipmentController/Create
        public ActionResult Create()
        {
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
            return View("DeleteShipment", model);
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
    }
}
