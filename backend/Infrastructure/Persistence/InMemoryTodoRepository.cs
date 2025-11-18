using Application.Interfaces;
using Domain.Entities;
using System.Collections.Concurrent;


namespace Infrastructure.Persistence;


public class InMemoryTodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<Guid, TodoItem> _store = new();


    public Task<TodoItem> AddAsync(TodoItem item, CancellationToken ct = default)
    {
        _store[item.Id] = item;
        return Task.FromResult(item);
    }


    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        return Task.FromResult(_store.TryRemove(id, out _));
    }


    public Task<IEnumerable<TodoItem>> GetAllAsync(CancellationToken ct = default)
    {
        return Task.FromResult(_store.Values.AsEnumerable());
    }

    public Task<TodoItem?> GetByIdAsync(Guid id)
    {
        _store.TryGetValue(id, out var item); 
        return Task.FromResult(item);
    }

    public Task<TodoItem> UpdateAsync(TodoItem item)
    {
        _store[item.Id] = item;
        return Task.FromResult(item);
    }
}