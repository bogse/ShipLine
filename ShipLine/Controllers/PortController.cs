using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;

namespace ShipLine.Controllers
{
    public class PortController : Controller
    {
        private PortRepository _portRepository;
        public PortController(ApplicationDbContext dbContext)
        {
            _portRepository = new PortRepository(dbContext);
        }
        // GET: PortController
        public ActionResult Index()
        {
            var list = _portRepository.GetAllPorts();
            return View(list);
        }

        // GET: PortController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _portRepository.GetPortById(id);
            return View("DetailsPort", model);
        }

        // GET: PortController/Create
        public ActionResult Create()
        {
            return View("CreatePort");
        }

        // POST: PortController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new PortModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _portRepository.InsertPort(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreatePort");
            }
        }

        // GET: PortController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _portRepository.GetPortById(id);
            return View("EditPort", model);
        }

        // POST: PortController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new PortModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _portRepository.UpdatePort(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("EditPort", id);
            }
        }

        // GET: PortController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _portRepository.GetPortById(id);
            return View("DeletePort", model);
        }

        // POST: PortController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _portRepository.DeletePort(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
