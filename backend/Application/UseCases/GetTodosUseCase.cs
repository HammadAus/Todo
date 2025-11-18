using Application.Interfaces;
public class GetTodosUseCase
{
    private readonly ITodoRepository _repo;
    public GetTodosUseCase(ITodoRepository repo) => _repo = repo;
    public Task<IEnumerable<Domain.Entities.TodoItem>> ExecuteAsync(CancellationToken ct = default) => _repo.GetAllAsync(ct);
}