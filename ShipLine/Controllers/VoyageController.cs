using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipLine.Controllers
{
    public class VoyageController : Controller
    {
        // GET: VoyageController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VoyageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VoyageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoyageController/Create
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

        // GET: VoyageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VoyageController/Edit/5
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

        // GET: VoyageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VoyageController/Delete/5
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
