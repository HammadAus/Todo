import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TodoItem } from '../../models/todo.model';
import { TodoService } from '../../services/todo.service';

@Component({
  selector: 'app-todo-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {
  newTask: string = '';
  todos: TodoItem[] = [];

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.todoService.getTodos().subscribe(data => this.todos = data);
  }

  addTodo(): void {
    if (!this.newTask.trim()) return;
    this.todoService.addTodo(this.newTask).subscribe(added => {
      this.todos.push(added);
      this.newTask = '';
    });
  }

  deleteTodo(id: string, index: number): void {
    this.todoService.deleteTodo(id).subscribe(() => this.todos.splice(index, 1));
  }

  toggleComplete(todo: TodoItem, index: number): void {
    this.todoService.toggleTodo(todo.id).subscribe(updated => {
      this.todos[index].isCompleted = updated.isCompleted;
    });
  }
}
