using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SubdivisionManagement.Model;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;
using Microsoft.EntityFrameworkCore;


public class AdminController : Controller
{
    private readonly HomeContext _context;

    public AdminController(HomeContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var admin = _context.Admins.FirstOrDefault(a => a.Username == username);


        if (admin == null || !VerifyPassword(password, admin.PasswordHash))
        {
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        HttpContext.Session.SetString("AdminUser", username);
        return RedirectToAction("Dashboard");
    }

    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("AdminUser") == null)
        {
            return RedirectToAction("Login");
        }

        return View("admin_dashboard");
    }

    public IActionResult Logout()
    {
        HttpContext.Session. Clear();
        return RedirectToAction("Login");
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        return HashPassword(password) == storedHash;
    }

    private string HashPassword(string password)
    {
        byte[] salt = Encoding.UTF8.GetBytes("password123");
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
    }

    public async Task<IActionResult> RegisterStaff([FromBody] Staff staff)
    {
        if (staff == null)
        {
            return BadRequest("Invalid data.");
        }

        // Generate a random salt for the password
        var salt = new byte[16];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password using the generated salt
        var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: staff.PasswordHash,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        // Store the hashed password and the salt in the database
        staff.PasswordHash = hashedPassword;
        staff.PasswordSalt = Convert.ToBase64String(salt);

        // Add the staff to the database
        _context.Staffs.Add(staff);
        await _context.SaveChangesAsync();

        return Json(new { message = "Staff successfully registered." });
    }
        
    public IActionResult Services()
    {
        if (HttpContext.Session.GetString("AdminUser") == null)
        {
            return RedirectToAction("Login");
        }
        return View("admin_services");  
    }

    // Get all service requests (for admin)
    [HttpGet]
    public IActionResult GetAllServiceRequests()
    {
        var requests = _context.Services
            .Include(s => s.Homeowner)
            .Include(s => s.AssignedStaff)
            .ToList();
        return Json(requests);
    }

    // Assign staff to a request
    [HttpPost]
    public IActionResult AssignStaff([FromBody] AssignStaffModel model)
    {
        var request = _context.Services.FirstOrDefault(s => s.Id == model.RequestId);
        if (request == null) return NotFound();

        request.AssignedStaffId = model.StaffId;
        request.Status = "in-progress";
        _context.SaveChanges();

        return Ok();
    }

    // Update status
    [HttpPost]
    public IActionResult UpdateRequestStatus([FromBody] UpdateStatusModel model)
    {
        var request = _context.Services.FirstOrDefault(s => s.Id == model.RequestId);
        if (request == null) return NotFound();

        request.Status = model.Status;
        _context.SaveChanges();

        return Ok();
    }

    public class AssignStaffModel
    {
        public int RequestId { get; set; }
        public int StaffId { get; set; }
    }

    public class UpdateStatusModel
    {
        public int RequestId { get; set; }
        public string Status { get; set; }
    }
}
