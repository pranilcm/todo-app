import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { TodoService, TodoItem } from './todo.service';

@Component({
  selector: 'app-root',
  imports: [DatePipe],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private readonly todoService = inject(TodoService);

  readonly todos = signal<TodoItem[]>([]);
  readonly loading = signal(true);
  readonly error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.loading.set(true);
    this.error.set(null);

    this.todoService.getTodos().subscribe({
      next: (todos) => {
        this.todos.set(todos);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Failed to load todos. Please try again.');
        this.loading.set(false);
      },
    });
  }

  addTodo(title: string): void {
    const trimmed = title.trim();
    if (!trimmed) return;

    this.todoService.createTodo(trimmed).subscribe({
      next: (created) => this.todos.update((todos) => [...todos, created]),
      error: () => this.error.set('Failed to add the todo. Please try again.'),
    });
  }

  deleteTodo(id: string): void {
    this.todoService.deleteTodo(id).subscribe({
      next: () => this.todos.update((todos) => todos.filter((t) => t.id !== id)),
      error: () => this.error.set('Failed to delete the todo. Please try again.'),
    });
  }
}
