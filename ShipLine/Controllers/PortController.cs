using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShipLine.Controllers
{
    public class PortController : Controller
    {
        // GET: PortController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PortController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PortController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PortController/Create
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

        // GET: PortController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PortController/Edit/5
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

        // GET: PortController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PortController/Delete/5
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
