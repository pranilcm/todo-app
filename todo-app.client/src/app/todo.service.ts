import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface TodoItem {
  id: string;
  title: string;
  createdAt: string;
}

const USER_ID_STORAGE_KEY = 'todo-app-user-id';

@Injectable({ providedIn: 'root' })
export class TodoService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/todos';

  //Random user id set in local storage
  private readonly headers = new HttpHeaders({ 'X-User-Id': this.getOrCreateUserId() });

  getTodos(): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]>(this.baseUrl, { headers: this.headers });
  }

  createTodo(title: string): Observable<TodoItem> {
    return this.http.post<TodoItem>(this.baseUrl, { title }, { headers: this.headers });
  }

  deleteTodo(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`, { headers: this.headers });
  }

  private getOrCreateUserId(): string {
    let id = localStorage.getItem(USER_ID_STORAGE_KEY);
    if (!id) {
      id = crypto.randomUUID();
      localStorage.setItem(USER_ID_STORAGE_KEY, id);
    }
    return id;
  }
}
