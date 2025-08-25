using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Quarky.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConceptsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ConceptsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Concept>>> GetConcepts()
    {
        return await _context.Concepts.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Concept>> GetConcept(int id)
    {
        var note = await _context.Concepts.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        return note;
    }

    [HttpPost]
    public async Task<ActionResult<Concept>> CreateConcept(Concept concept)
    {
        concept.Id = 0;
        _context.Concepts.Add(concept);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetConcept), new { id = concept.Id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConcept(int id, Concept concept)
    {
        if (id != concept.Id) return BadRequest();

        _context.Entry(concept).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null) return NotFound();

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}/dependencies")]
    public async Task<ActionResult<IEnumerable<Concept>>> GetDependencies(int id)
    {
        var concept = await _context.Concepts
            .Include(c => c.Dependencies)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (concept == null) return NotFound();
        return Ok(concept);
    }

    [HttpPost("{id}/dependencies/{dependencyId}")]
    public async Task<IActionResult> AddDependency(int id, int dependencyId)
    {
        var concept = await _context.Concepts
            .Include(c => c.Dependencies)
            .FirstOrDefaultAsync(c => c.Id == id);

        var dependency = await _context.Concepts.FindAsync(dependencyId);

        if(concept == null || dependency == null) return NotFound();
        if (!concept.Dependencies.Contains(dependency))
        {
            concept.Dependencies.Add(dependency);
            await _context.SaveChangesAsync();
        }
        return NoContent();
    }
}

