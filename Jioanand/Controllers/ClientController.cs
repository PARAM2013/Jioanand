using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jioanand.Data;
using Jioanand.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;

namespace Jioanand.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClientController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Client
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var clients = from c in _context.Clients
                          select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(c => c.FullName.Contains(searchString)
                                       || c.ContactNumber.Contains(searchString));
            }

            var clientList = await clients.OrderByDescending(c => c.UpdatedAt).ToListAsync();
            return View(clientList);
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                // Eagerly load related documents and bookings for the details view
                .Include(c => c.Documents)
                .Include(c => c.Bookings)
                .FirstOrDefaultAsync(m => m.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,ContactNumber,Email,Address,AlternateContact")] Client client, IFormFile documentFile, string documentType)
        {
            if (ModelState.IsValid)
            {
                client.CreatedAt = DateTime.UtcNow;
                client.UpdatedAt = DateTime.UtcNow;
                _context.Add(client);
                await _context.SaveChangesAsync();

                if (documentFile != null)
                {
                    await UploadDocumentAsync(documentFile, client.ClientId, documentType);
                }

                TempData["SuccessMessage"] = "Client created successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "The form has errors. Please correct them and try again.";
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,FullName,ContactNumber,Email,Address,AlternateContact,CreatedAt")] Client client, IFormFile documentFile, string documentType)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    client.UpdatedAt = DateTime.UtcNow;
                    _context.Update(client);
                    await _context.SaveChangesAsync();

                    if (documentFile != null)
                    {
                        await UploadDocumentAsync(documentFile, client.ClientId, documentType);
                    }

                    TempData["SuccessMessage"] = "Client updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
                    {
                        TempData["ErrorMessage"] = "The client you were editing was deleted.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The client was modified by another user. Please try again.");
                    }
                }
            }
            TempData["ErrorMessage"] = "The form has errors. Please correct them and try again.";
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                try
                {
                    // Note: This does not delete files from the server. A more robust implementation would handle that.
                    var documents = _context.Documents.Where(d => d.ClientId == id);
                    _context.Documents.RemoveRange(documents);

                    _context.Clients.Remove(client);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Client deleted successfully.";
                }
                catch(DbUpdateException)
                {
                    TempData["ErrorMessage"] = "This client cannot be deleted as they have existing bookings.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Client not found.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task UploadDocumentAsync(IFormFile file, int clientId, string documentType)
        {
            if (file.Length > 5 * 1024 * 1024) // 5 MB limit
            {
                ModelState.AddModelError("documentFile", "The file size cannot exceed 5MB.");
                return;
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("documentFile", "Invalid file type. Only JPG, PNG, and PDF are allowed.");
                return;
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/documents");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var document = new Document
            {
                ClientId = clientId,
                DocumentType = string.IsNullOrEmpty(documentType) ? "Unspecified" : documentType,
                FilePath = "/uploads/documents/" + uniqueFileName, // Store relative path
                UploadedAt = DateTime.UtcNow
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
