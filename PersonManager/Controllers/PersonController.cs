using PersonManager.Dao;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PersonManager.Controllers
{
    public class PersonController : Controller
    {
        private static readonly ICosmosDbService service = CosmosDbServiceProvider.CosmosDbService;
        public async Task<ActionResult> Index()
        {
            return View(await service.GetPeopleAsync("SELECT * FROM People"));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid().ToString();
                await service.AddPersonAsync(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public async Task<ActionResult> Edit(string id) => await ShowItem(id);

        private async Task<ActionResult> ShowItem(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var item = await service.GetPersonAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id, FirstName, LastName, Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                await service.UpdatePersonAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public async Task<ActionResult> Delete(string id) => await ShowItem(id);

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed([Bind(Include = "Id,FirstName,LastName,Email")] Person person)
        {
            await service.DeletePersonAsync(person);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id) => await ShowItem(id);
    }
}