using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipLine.Controllers
{
    public class ShipController : Controller
    {
        // GET: ShipController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShipController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShipController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShipController/Create
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

        // GET: ShipController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShipController/Edit/5
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

        // GET: ShipController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShipController/Delete/5
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
