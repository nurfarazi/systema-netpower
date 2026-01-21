import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
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
  private totalCountSubject = new BehaviorSubject<number>(0);

  loading$ = this.loadingSubject.asObservable();
  users$ = this.usersSubject.asObservable();
  totalCount$ = this.totalCountSubject.asObservable();

  private getUsers(search?: string, isActive?: boolean, page: number = 1, pageSize: number = 25): void {
    this.loadingSubject.next(true);

    let params = new HttpParams();
    params = params.set('page', page.toString());
    params = params.set('pageSize', pageSize.toString());
    if (search) {
      params = params.set('search', search);
    }
    if (isActive !== undefined) {
      params = params.set('isActive', isActive.toString());
    }

    this.http
      .get<UsersResponse>(`${environment.apiUrl}/Users`, { params })
      .pipe(
        tap((response) => {
          this.usersSubject.next(response.items);
          this.totalCountSubject.next(response.totalCount);
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

  refreshUsers(search?: string, isActive?: boolean, page: number = 1, pageSize: number = 25): void {
    this.getUsers(search, isActive, page, pageSize);
  }
}
