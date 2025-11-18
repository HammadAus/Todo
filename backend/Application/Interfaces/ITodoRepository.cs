using Domain.Entities;


namespace Application.Interfaces;


public interface ITodoRepository
{
Task<IEnumerable<TodoItem>> GetAllAsync(CancellationToken ct = default);
Task<TodoItem> AddAsync(TodoItem item, CancellationToken ct = default);
Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
Task<TodoItem?> GetByIdAsync(Guid id);
Task<TodoItem> UpdateAsync(TodoItem item); 
}