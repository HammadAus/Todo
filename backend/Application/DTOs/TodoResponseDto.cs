namespace Application.DTOs;

public record TodoResponseDto(Guid Id, string Title, bool IsCompleted);