using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
      private readonly MyAppContext _context;

        public ItemsController(MyAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var item = await _context.items.ToListAsync();
            return View(item);
        }

        //create
        public IActionResult Create() {  // it return view

            return View();
        }

        [HttpPost]  // this action happen when we click on button in crete.cshtml
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");// after add to list then open index page to show list
            }

            return View(item); // return current view (ceate.cshtml)
        }


        // return edit view
        public async Task<IActionResult> Edit(int id) // create edit View
        {
            var item = await _context.items.FirstOrDefaultAsync(x=> x.Id == id);
            return View(item);
        }

        [HttpPost]  // this action happen when we click on button in edit.cshtml
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price")] Item item)
        {
            if (ModelState.IsValid) { 
              _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(item);

        }

        public async Task<IActionResult> Delete(int id) // return delete View
        {
            var item = await _context.items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.items.FindAsync(id);
            if(item != null)
            {
                _context.items.Remove(item);    
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
