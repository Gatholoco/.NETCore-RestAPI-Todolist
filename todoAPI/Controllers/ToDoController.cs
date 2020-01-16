using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoAPI.Models;
using todoAPI.ViewModel;

namespace todoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly todoAPIContext _context;

        public ToDoController(todoAPIContext context)
        {
            _context = context;
        }

        /* GET ALL TODO */
        // GET: api/ToDo
        [HttpGet]
        public IEnumerable<ToDoModel> GetToDoModel()
        {
            return _context.ToDoModel;
        }

        /* GET Specific TODO by ID */
        // GET: api/ToDo/id/5
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetToDoModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDoModel = await _context.ToDoModel.FindAsync(id);

            if (toDoModel == null)
            {
                return NotFound();
            }

            return Ok(toDoModel);
        }

        /* GET Specific TODO by Date to get Data today/next day/current week */
        // GET: api/ToDo/date/yyyy-mm-dd/yyyy-mm-dd
        [HttpGet("date/{StartDate}/{EndDate?}")]
        public async Task<IActionResult> GetToDoModelbyDate([FromRoute] DateTime StartDate, DateTime? EndDate = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDoModel = EndDate == null ? 
                await _context.ToDoModel.Where(x => x.StartDate.Date == StartDate.Date).ToListAsync()
                : await _context.ToDoModel.Where(x => x.StartDate.Date == StartDate.Date && x.EndDate.Date == EndDate.Value.Date).ToListAsync();

            if (toDoModel == null)
            {
                return NotFound();
            }

            return Ok(toDoModel);
        }
        
        /* UPDATE Todo */
        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoModel([FromRoute] int id, [FromBody] ToDoModel toDoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDoModel.Id)
            {
                return BadRequest();
            }
            
            var isDone = false;
            toDoModel.isDone = toDoModel.Percentage == 100 ? !isDone : isDone;
            _context.Entry(toDoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoModelExists(id))
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

        /* CREATE ToDo */
        // POST: api/ToDo
        [HttpPost]
        public async Task<IActionResult> PostToDoModel([FromBody] ToDoModel toDoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isDone = false;
            toDoModel.isDone = toDoModel.Percentage == 100 ? !isDone : isDone;


            _context.ToDoModel.Add(toDoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoModel", new { id = toDoModel.Id }, toDoModel);
        }
        
        /* DELETE TODO */
        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDoModel = await _context.ToDoModel.FindAsync(id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            _context.ToDoModel.Remove(toDoModel);
            await _context.SaveChangesAsync();

            return Ok(toDoModel);
        }

        private bool ToDoModelExists(int id)
        {
            return _context.ToDoModel.Any(e => e.Id == id);
        }
    }
}