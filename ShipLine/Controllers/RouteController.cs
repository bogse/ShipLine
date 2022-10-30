using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;

namespace ShipLine.Controllers
{
    public class RouteController : Controller
    {
        private RouteRepository _routeRepository;
        public RouteController(ApplicationDbContext dbContext)
        {
            _routeRepository = new RouteRepository(dbContext);
        }

        // GET: RouteController
        public ActionResult Index()
        {
            var list = _routeRepository.GetAllRoutes();
            return View(list);
        }

        // GET: RouteController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _routeRepository.GetRouteModelById(id);
            return View("DetailsRoute", model);
        }

        // GET: RouteController/Create
        public ActionResult Create()
        {
            return View("CreateRoute");
        }

        // POST: RouteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new RouteModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _routeRepository.InsertRoute(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateRoute");
            }
        }

        // GET: RouteController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _routeRepository.GetRouteModelById(id);
            return View("EditRoute", model);
        }

        // POST: RouteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new RouteModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _routeRepository.UpdateRoute(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: RouteController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _routeRepository.GetRouteModelById(id);
            return View("DeleteRoute", model);
        }

        // POST: RouteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _routeRepository.DeleteRoute(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
