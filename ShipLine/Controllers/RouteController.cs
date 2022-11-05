using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;
using ShipLine.ViewModel;

namespace ShipLine.Controllers
{
    public class RouteController : Controller
    {
        private RouteRepository _routeRepository;
        private PortRepository _portRepository;

        public RouteController(ApplicationDbContext dbContext)
        {
            _routeRepository = new RouteRepository(dbContext);
            _portRepository = new PortRepository(dbContext);
        }

        // GET: RouteController
        public ActionResult Index()
        {
            var list = _routeRepository.GetAllRoutes();
            var viewModelList = new List<RouteViewModel>();
            foreach (var route in list)
            {
                viewModelList.Add(new RouteViewModel(route, _portRepository));
            }

            return View(viewModelList);
        }

        // GET: RouteController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _routeRepository.GetRouteById(id);
            var viewModel = new RouteViewModel(model, _portRepository);

            return View("DetailsRoute", viewModel);
        }

        // GET: RouteController/Create
        public ActionResult Create()
        {
            var list = _portRepository.GetAllPorts();
            var portList = list.Select(x => new SelectListItem(x.Name, x.PortId.ToString()));
            ViewBag.PortList = portList;

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
            var model = _routeRepository.GetRouteById(id);
            var list = _portRepository.GetAllPorts();
            var portList = list.Select(x => new SelectListItem(x.Name, x.PortId.ToString()));
            ViewBag.PortList = portList;

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
            var model = _routeRepository.GetRouteById(id);
            var viewModel = new RouteViewModel(model, _portRepository);

            return View("DeleteRoute", viewModel);
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
