namespace Domain.Entities;


public sealed class TodoItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }


    public TodoItem(string title)
    {
        Id = Guid.NewGuid();
        Title = title ?? throw new ArgumentNullException(nameof(title));
        IsCompleted = false;
    }


    public void ToggleCompleted() => IsCompleted = !IsCompleted;
}