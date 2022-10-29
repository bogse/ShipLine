using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipLine.Controllers
{
    public class ShipmentController : Controller
    {
        // GET: ShipmentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShipmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShipmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShipmentController/Create
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

        // GET: ShipmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShipmentController/Edit/5
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

        // GET: ShipmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShipmentController/Delete/5
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
