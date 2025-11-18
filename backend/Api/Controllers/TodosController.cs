using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _repo;
        public TodosController(ITodoRepository repo) => _repo = repo;


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var todos = await _repo.GetAllAsync();
            var result = todos.Select(t => new { t.Id, t.Title, t.IsCompleted });
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateDto dto)
        {
            var errors = ValidateTodoCreateDto(dto);
            if (errors.Any())
            {
                Log.Warning("Validation failed: {Errors}", string.Join(", ", errors));
                return BadRequest(errors);
            }

            Log.Information("Creating todo with title: {Title}", dto.Title);
            var item = new TodoItem(dto.Title);
            var added = await _repo.AddAsync(item);
            Log.Information("Todo created with ID: {Id}", added.Id);
            return CreatedAtAction(nameof(Get), new { id = added.Id }, new TodoResponseDto( added.Id, added.Title, added.IsCompleted ));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return NotFound();

            Log.Information("Deleted todo with ID: {Id}", id);
            return NoContent();
        }

        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> ToggleComplete(Guid id)
        {
            var todo = await _repo.GetByIdAsync(id);
            if (todo == null) return NotFound();

            todo.ToggleCompleted();
            await _repo.UpdateAsync(todo);

            Log.Information("Toggled completion for todo ID: {Id}", id);
            return Ok(new TodoResponseDto( todo.Id, todo.Title, todo.IsCompleted ));
        }

        private List<string> ValidateTodoCreateDto(TodoCreateDto dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Title))
                errors.Add("Title is required.");

            if (dto.Title.Length > 100)
                errors.Add("Title cannot exceed 100 characters.");

            return errors;
        }
    }
}