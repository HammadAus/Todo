using System;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers;
using Xunit;

namespace Api.Tests;

public class TodosControllerTests
{
    private readonly ITodoRepository _repo;
    private readonly TodosController _controller;

    public TodosControllerTests()
    {
        _repo = new InMemoryTodoRepository();
        _controller = new TodosController(_repo);
    }

    [Fact]
    public async Task Get_WhenNoTodos_ReturnsEmptyList()
    {
        var result = await _controller.Get() as OkObjectResult;

        result.Should().NotBeNull();
        var todos = result!.Value as IEnumerable<object>;
        todos.Should().BeEmpty();
    }

    [Fact]
    public async Task Create_ValidTodo_ReturnsCreatedTodo()
    {
        var dto = new TodoCreateDto("Test Todo");

        var result = await _controller.Create(dto) as CreatedAtActionResult;

        result.Should().NotBeNull();
        var createdTodo = result!.Value as TodoResponseDto;
        createdTodo!.Title.Should().Be("Test Todo");
        createdTodo!.IsCompleted.Should().BeFalse();

        var allTodos = await _repo.GetAllAsync();
        allTodos.Count().Should().Be(1);
    }

    [Fact]
    public async Task Create_InvalidTodo_ReturnsBadRequest()
    {
        var dto = new TodoCreateDto("");

        var result = await _controller.Create(dto) as BadRequestObjectResult;

        result.Should().NotBeNull();
        var errors = result!.Value as IEnumerable<string>;
        errors!.Should().Contain("Title is required.");
    }

    [Fact]
    public async Task Delete_ExistingTodo_ReturnsNoContent()
    {
        var item = new TodoItem("Delete Test");
        await _repo.AddAsync(item);

        var result = await _controller.Delete(item.Id);

        result.Should().BeOfType<NoContentResult>();
        (await _repo.GetAllAsync()).Should().BeEmpty();
    }

    [Fact]
    public async Task ToggleComplete_ChangesIsCompletedStatus()
    {
        var item = new TodoItem("Toggle Test");
        await _repo.AddAsync(item);

        var result = await _controller.ToggleComplete(item.Id) as OkObjectResult;

        result.Should().NotBeNull();
        var toggledTodo = result!.Value as TodoResponseDto;
        toggledTodo!.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task ToggleComplete_NonExistingTodo_ReturnsNotFound()
    {
        var result = await _controller.ToggleComplete(Guid.NewGuid());

        result.Should().BeOfType<NotFoundResult>();
    }
}
