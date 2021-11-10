using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DapperDemo.Data;
using DapperDemo.Models;
using DapperDemo.Repository;

namespace DapperDemo.Controllers
{
    public class EmployeesController : Controller
    {
        //   private readonly ApplicationDbContext _context;
        private readonly ICompanyRepository _compRepo;
        private readonly IEmployeeRepository _empRepo;



        public EmployeesController(ICompanyRepository compRepo , IEmployeeRepository empRepo)
        {
            _compRepo = compRepo;
            _empRepo = empRepo; 

         
        }

        [BindProperty]
        public Employee Employee { get; set; }
        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(_empRepo.GetAll()) ;
        }

        // GET: Companies/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var company = _compRepo.find(id.GetValueOrDefault());

            
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(company);
        //}

        // GET: Companies/Create
        public IActionResult Create()
        {
           IEnumerable<SelectListItem> companyList = _compRepo.GetAll().Select(i => new SelectListItem
            {
               Text = i.Name,
                Value = i.CompanyId.ToString()
            }) ;
            ViewBag.CompanyList = companyList;

            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST()
        {
            if (ModelState.IsValid)
            {
                _empRepo.Add(Employee);

                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = _empRepo.find(id.GetValueOrDefault());

            if (Employee == null)
            {
                return NotFound();
            }
            return View(Employee);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != Employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _empRepo.Update(Employee);
                 
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _empRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));

        }

    
       
      
    }
}
