using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;

namespace ShipLine.Controllers
{
    public class VoyageController : Controller
    {
        private VoyageRepository _voyageRepository;
        public VoyageController(ApplicationDbContext dbContext)
        {
            _voyageRepository = new VoyageRepository(dbContext);
        }
        // GET: VoyageController
        public ActionResult Index()
        {
            var list = _voyageRepository.GetAllVoyages();
            return View("Index", list);
        }

        // GET: VoyageController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _voyageRepository.GetVoyageById(id);
            return View("DetailsVoyage", model);
        }

        // GET: VoyageController/Create
        public ActionResult Create()
        {
            return View("CreateVoyage");
        }

        // POST: VoyageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new VoyageModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _voyageRepository.InsertVoyage(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateVoyage");
            }
        }

        // GET: VoyageController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _voyageRepository.GetVoyageById(id);
            return View("EditVoyage", model);
        }

        // POST: VoyageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new VoyageModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _voyageRepository.UpdateVoyage(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: VoyageController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _voyageRepository.GetVoyageById(id);
            return View("DeleteVoyage", model);
        }

        // POST: VoyageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _voyageRepository.DeleteVoyage(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
