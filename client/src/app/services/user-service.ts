import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, finalize, tap } from 'rxjs';
import { of } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { User, UsersResponse } from '../models/user.interface';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private http = inject(HttpClient);

  private loadingSubject = new BehaviorSubject<boolean>(false);
  private usersSubject = new BehaviorSubject<User[]>([]);

  loading$ = this.loadingSubject.asObservable();
  users$ = this.usersSubject.asObservable();

  private getUsers(): void {
    this.loadingSubject.next(true);

    this.http
      .get<UsersResponse>(`${environment.apiUrl}/Users`)
      .pipe(
        tap((response) => {
          this.usersSubject.next(response.items);
        }),
        catchError((error) => {
          console.error('Error fetching users:', error);
          this.usersSubject.next([]);
          return of(null);
        }),
        finalize(() => {
          this.loadingSubject.next(false);
        })
      )
      .subscribe();
  }

  refreshUsers(): void {
    this.getUsers();
  }
}
