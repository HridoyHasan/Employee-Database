using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Where(e => !e.IsDeleted).ToListAsync();
            return View(employees);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Employee employee, IFormFile? ProfilePicture)
{
    if (ModelState.IsValid)
    {
        // Handle profile picture upload if provided
        if (ProfilePicture != null && ProfilePicture.Length > 0)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfilePicture.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await ProfilePicture.CopyToAsync(fileStream);
            }

            // Save the relative path in the database
            employee.ProfilePicturePath = "/images/" + uniqueFileName;
        }

        // Set any missing optional fields to default values if necessary
        employee.Name = !string.IsNullOrWhiteSpace(employee.Name) ? employee.Name : "N/A";
        employee.NID = !string.IsNullOrWhiteSpace(employee.NID) ? employee.NID : null;
        employee.Organization = !string.IsNullOrWhiteSpace(employee.Organization) ? employee.Organization : null;
        employee.Expertise = !string.IsNullOrWhiteSpace(employee.Expertise) ? employee.Expertise : null;
        employee.CriminalRecord = !string.IsNullOrWhiteSpace(employee.CriminalRecord) ? employee.CriminalRecord : null;

        // Add the employee to the context
        _context.Add(employee);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // If model state is invalid, return the view with the existing data
    return View(employee);
}


        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employee/Edit/5
     [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, Employee employee, IFormFile? ProfilePicture)
{
    if (id != employee.Id) return NotFound();

    if (ModelState.IsValid)
    {
        try
        {
            // Retrieve the existing employee from the database
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null) return NotFound();

            // Update the non-null fields from the form
            existingEmployee.Name = employee.Name;
            existingEmployee.NID = employee.NID;
            existingEmployee.Age = employee.Age;
            existingEmployee.Organization = employee.Organization;
            existingEmployee.Expertise = employee.Expertise;
            existingEmployee.CriminalRecord = employee.CriminalRecord;

            // Handle profile picture upload if provided
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfilePicture.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfilePicture.CopyToAsync(fileStream);
                }

                // Update the profile picture path
                existingEmployee.ProfilePicturePath = "/images/" + uniqueFileName;
            }

            // Mark the entity as modified
            _context.Update(existingEmployee);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(employee.Id)) return NotFound();
            else throw;
        }

        return RedirectToAction(nameof(Index));
    }
    return View(employee);
}

   // GET: Employee/Delete/5
public async Task<IActionResult> Delete(int? id)
{
    if (id == null) return NotFound();

    var employee = await _context.Employees.FindAsync(id);
    if (employee == null) return NotFound();

    return View(employee);
}

// POST: Employee/Delete/5
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var employee = await _context.Employees.FindAsync(id);
    if (employee != null)
    {
        employee.IsDeleted = true;  // Soft delete
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Index));
}


        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
