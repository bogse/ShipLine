using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;
using System.Data;

namespace ShipLine.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ShipController : Controller
    {
        private ShipRepository _shipRepository;
        public ShipController(ApplicationDbContext dbContext)
        {
            _shipRepository = new ShipRepository(dbContext);
        }
        // GET: ShipController
        public ActionResult Index()
        {
            var list = _shipRepository.GetAllShips();
            return View("Index", list);
        }

        // GET: ShipController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _shipRepository.GetShipById(id);
            return View("DetailsShip", model);
        }

        // GET: ShipController/Create
        public ActionResult Create()
        {
            return View("CreateShip");
        }

        // POST: ShipController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new ShipModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _shipRepository.InsertShip(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateShip");
            }
        }

        // GET: ShipController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _shipRepository.GetShipById(id);
            return View("EditShip", model);
        }

        // POST: ShipController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new ShipModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _shipRepository.UpdateShip(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: ShipController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _shipRepository.GetShipById(id);
            return View("DeleteShip", model);
        }

        // POST: ShipController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _shipRepository.DeleteShip(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
