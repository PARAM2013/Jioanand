using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jioanand.Data;
using Jioanand.Models;
using Jioanand.Models.Enums;

namespace Jioanand.Controllers;

public class RoomController : Controller
{
    private readonly ApplicationDbContext _context;

    public RoomController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Room
    public async Task<IActionResult> Index()
    {
        var rooms = await _context.Rooms
            .Include(r => r.Location)
            .OrderBy(r => r.Location.Name)
            .ThenBy(r => r.Floor)
            .ThenBy(r => r.RoomNumber)
            .ToListAsync();

        return View(rooms);
    }

    // GET: Room/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms
            .Include(r => r.Location)
            .FirstOrDefaultAsync(m => m.RoomId == id);

        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // GET: Room/Create
    public IActionResult Create()
    {
        ViewBag.Locations = _context.Locations.ToList();
        ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
            .Cast<RoomType>()
            .Select(t => new { Id = (int)t, Name = t.ToString() });
        return View();
    }

    // POST: Room/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LocationId,RoomNumber,Floor,Type,Capacity,PricePerDay,Description")] Room room)
    {
        if (ModelState.IsValid)
        {
            room.Status = RoomStatus.Available;
            room.CreatedAt = DateTime.UtcNow;
            room.UpdatedAt = DateTime.UtcNow;
            _context.Add(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Locations = _context.Locations.ToList();
        ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
            .Cast<RoomType>()
            .Select(t => new { Id = (int)t, Name = t.ToString() });
        return View(room);
    }

    // GET: Room/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }

        ViewBag.Locations = _context.Locations.ToList();
        ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
            .Cast<RoomType>()
            .Select(t => new { Id = (int)t, Name = t.ToString() });
        ViewBag.RoomStatuses = Enum.GetValues(typeof(RoomStatus))
            .Cast<RoomStatus>()
            .Select(s => new { Id = (int)s, Name = s.ToString() });
        return View(room);
    }

    // POST: Room/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("RoomId,LocationId,RoomNumber,Floor,Type,Capacity,PricePerDay,Description,Status")] Room room)
    {
        if (id != room.RoomId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                room.UpdatedAt = DateTime.UtcNow;
                _context.Update(room);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(room.RoomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Locations = _context.Locations.ToList();
        ViewBag.RoomTypes = Enum.GetValues(typeof(RoomType))
            .Cast<RoomType>()
            .Select(t => new { Id = (int)t, Name = t.ToString() });
        ViewBag.RoomStatuses = Enum.GetValues(typeof(RoomStatus))
            .Cast<RoomStatus>()
            .Select(s => new { Id = (int)s, Name = s.ToString() });
        return View(room);
    }

    // GET: Room/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms
            .Include(r => r.Location)
            .FirstOrDefaultAsync(m => m.RoomId == id);
        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // POST: Room/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room != null)
        {
            _context.Rooms.Remove(room);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Room/Availability
    public async Task<IActionResult> Availability(DateTime? date)
    {
        date ??= DateTime.Today;

        var rooms = await _context.Rooms
            .Include(r => r.Location)
            .Include(r => r.BookingRooms)
                .ThenInclude(br => br.Booking)
            .OrderBy(r => r.Location.Name)
            .ThenBy(r => r.Floor)
            .ThenBy(r => r.RoomNumber)
            .ToListAsync();

        ViewBag.Date = date.Value;
        return View(rooms);
    }

    private bool RoomExists(int id)
    {
        return _context.Rooms.Any(e => e.RoomId == id);
    }
}
