using Microsoft.AspNetCore.Mvc;
using Products_CRUD.Models;
using Products_CRUD.Repo;

namespace Products_CRUD.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProducts productRepo;
        public ProductsController(IProducts product)
        {
            this.productRepo = product;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productRepo.Get();
            return View(products);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var product = await productRepo.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products model)
        {
            if (ModelState.IsValid)
            {
                await productRepo.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var product = await productRepo.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Products model)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var product = await productRepo.Find(id);

            if (product == null)
            {
                return BadRequest();
            }
            await productRepo.Update(model);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var product = await productRepo.Find(id);

            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var product = await productRepo.Find(id);
            await productRepo.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}

