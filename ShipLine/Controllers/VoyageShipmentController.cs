using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipLine.Controllers
{
    public class VoyageShipmentController : Controller
    {
        // GET: VoyageShipmentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VoyageShipmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VoyageShipmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoyageShipmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VoyageShipmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VoyageShipmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VoyageShipmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VoyageShipmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
