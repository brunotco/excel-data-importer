using ExcelDataImporterDatabase.Models;
using ExcelDataImporterDatabase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExcelDataImporterCore;

namespace ExcelDataImporterApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DatabaseContext _context;

    public UserController(DatabaseContext context)
    {
        _context = context;
    }
    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUser()
    {
        return await _context.User.ToListAsync();
    }

    // GET: api/User/active
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<User>>> GetActive()
    {
        //var data = await _context.User.ToListAsync();
        return _context.User.Where((dbPlanning) => dbPlanning.Active.Equals(true)).ToList();
    }

    //GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var data = await _context.User.FindAsync(id);

        if (data == null)
        {
            return NotFound();
        }

        return data;
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlanning(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        user = Utils.ValidateUser(user);

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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

    // PUT: api/User/inactive/5
    [HttpPut("inactive/{id}")]
    public async Task<IActionResult> InactiveUser(int id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        user.Active = false;

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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

    // PUT: api/User/active/5
    [HttpPut("active/{id}")]
    public async Task<IActionResult> ActiveUser(int id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        user.Active = true;

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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

    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        user = Utils.ValidateUser(user);
        User existingUser = _context.User.Where((dbPlanning) => dbPlanning.Username.Equals(user.Username)).FirstOrDefault();
        if (existingUser == null)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        else
        {
            //return BadRequest(existingUser);
            await this.ActiveUser((int)existingUser.Id);
            return AcceptedAtAction("GetUser", existingUser);
        }

    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.User.Any(e => e.Id == id);
    }
}