using LastEmployeeTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace LastEmployeeTest.Controllers
{
    public class EmployeesController1 : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotify;
        public EmployeesController1(ApplicationDbContext context , IToastNotification toastNotify)
        {
            _context = context;
            _toastNotify = toastNotify;
        }
        public async Task< IActionResult > Index()
        {
            var employee = await _context.Employees.Include(m=>m.Department).ToListAsync();
            //ViewBag.Employees = await _context.Departments.OrderBy(m=>m.DeptName).ToListAsync();
            return View(employee);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.employee= await _context.Departments.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(Employee model)
        {
            if (ModelState.IsValid) 
            {
                _context.Employees.AddAsync(model);
                _toastNotify.AddSuccessToastMessage("Employee Created Succefully");
                _context.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
            ViewBag.employee = await _context.Departments.ToListAsync();
            return View(nameof(Create));
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Employees.FindAsync(id);
            ViewBag.employee = await _context.Departments.ToListAsync();
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(model);
                _toastNotify.AddSuccessToastMessage("Employee Updated Successfully");
                _context.SaveChanges();
               return RedirectToAction(nameof(Index));
            }
            ViewBag.employee = await _context.Departments.ToListAsync();
            return View("Create", model);
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(model);
            _context.SaveChanges();
             return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var model = await _context.Employees.Include(m=>m.Department).SingleOrDefaultAsync(m=>m.Id== id);
            return View("Details", model);
        }
    }
}
