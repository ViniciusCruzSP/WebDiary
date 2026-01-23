using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDiaryAPI.Data;
using WebDiaryAPI.Models;

namespace WebDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DiaryEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryEntry>>> GetDiaryEntries()
        {
            return await _context.DiaryEntries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntry>> GetDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);

            if (diaryEntry == null)
            {
                return NotFound();
            }
            return diaryEntry;
        }

        [HttpPost]
        public async Task<ActionResult<DiaryEntry>> PostDiaryEntry(DiaryEntry diaryEntry)
        {
            diaryEntry.Id = 0;
            _context.DiaryEntries.Add(diaryEntry);
            await _context.SaveChangesAsync();
            var resourceUrl = Url.Action(nameof(GetDiaryEntry), new { id = diaryEntry.Id });
            return Created(resourceUrl, diaryEntry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiaryEntry(int id, [FromBody] DiaryEntry diaryEntry)
        {
            if (id != diaryEntry.Id)
            {
                return BadRequest();
            }
            _context.Entry(diaryEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaryEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);
            if (diaryEntry == null)
            {
                return NotFound();
            }
            _context.DiaryEntries.Remove(diaryEntry);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool DiaryEntryExists(int id)
        {
            return _context.DiaryEntries.Any(e => e.Id == id);
        }

        private ActionResult? ValidateDiaryEntry(DiaryEntry entry)
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(entry.Title))
                errors["Title"] = ["Title is Required"];
            else if (entry.Title.Length < 3)
                errors["Title"] = ["Title must have at least 3 characters"];

            if (string.IsNullOrWhiteSpace(entry.Content))
                errors["Content"] = ["Content is Required"];
            else if (entry.Content.Length < 10)
                errors["Title"] = ["Content must have at least 10 characters"];

            if (entry.Created > DateTime.UtcNow)
                errors["Created"] = ["Date cannot be in the future."];

            if (errors.Any())
                return BadRequest(new ApiValidationProblemDetails(errors));

            return null;
        }
    }
}
