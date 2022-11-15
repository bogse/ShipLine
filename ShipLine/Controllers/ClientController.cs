using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipLine.Data;
using ShipLine.Models;
using ShipLine.Repository;

namespace ShipLine.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ClientController : Controller
    {
        private ClientRepository _clientRepository;
        public ClientController(ApplicationDbContext dbContext)
        {
            _clientRepository = new ClientRepository(dbContext);
        }
        // GET: ClientController
        public ActionResult Index()
        {
            var list = _clientRepository.GetAllClients();
            return View(list);
        }

        // GET: ClientController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _clientRepository.GetClientById(id);
            return View("DetailsClient", model);
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View("CreateClient");
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new ClientModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _clientRepository.InsertClient(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateClient");
            }
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _clientRepository.GetClientById(id);
            return View("EditClient", model);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new ClientModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _clientRepository.UpdateClient(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit", id);
            }
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _clientRepository.GetClientById(id);
            return View("DeleteClient", model);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _clientRepository.DeleteClient(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
