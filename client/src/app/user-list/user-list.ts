import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../services/user-service.js';
import { User } from '../models/user.interface';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-list.html',
  styleUrl: './user-list.scss'
})
export class UserList implements OnInit, OnDestroy {
  private userService = inject(UserService);
  private destroy$ = new Subject<void>();
  private searchSubject = new Subject<string>();

  protected users$: Observable<User[]>;
  protected loading$: Observable<boolean>;
  protected totalCount$: Observable<number>;
  protected searchTerm: string = '';
  protected isActiveFilter: string = 'all'; // 'all', 'active', 'inactive'
  protected currentPage: number = 1;
  protected pageSize: number = 25;
  protected totalItems: number = 0;
  protected pageSizeOptions: number[] = [10, 25, 50, 100];

  constructor() {
    this.users$ = this.userService.users$;
    this.loading$ = this.userService.loading$;
    this.totalCount$ = this.userService.totalCount$;
  }

  ngOnInit(): void {
    // Set up search debouncing
    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe(searchTerm => {
      this.currentPage = 1; // Reset to first page on search
      this.applyFilters();
    });

    // Subscribe to totalCount changes
    this.totalCount$.pipe(
      takeUntil(this.destroy$)
    ).subscribe(count => {
      this.totalItems = count;
    });

    // Initial load
    this.applyFilters();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  protected onSearchChange(searchTerm: string): void {
    this.searchTerm = searchTerm;
    this.searchSubject.next(searchTerm);
  }

  protected onIsActiveFilterChange(): void {
    this.currentPage = 1; // Reset to first page on filter change
    this.applyFilters();
  }

  protected clearFilters(): void {
    this.searchTerm = '';
    this.isActiveFilter = 'all';
    this.currentPage = 1;
    this.applyFilters();
  }

  private applyFilters(): void {
    const search = this.searchTerm.trim() || undefined;
    const isActive = this.isActiveFilter === 'all' ? undefined : this.isActiveFilter === 'active';

    this.userService.refreshUsers(search, isActive, this.currentPage, this.pageSize);
  }

  protected trackByUserId(index: number, user: User): number {
    return user.id;
  }

  protected get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  protected get startItem(): number {
    return this.totalItems === 0 ? 0 : (this.currentPage - 1) * this.pageSize + 1;
  }

  protected get endItem(): number {
    return Math.min(this.currentPage * this.pageSize, this.totalItems);
  }

  protected nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.applyFilters();
    }
  }

  protected prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.applyFilters();
    }
  }

  protected goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.applyFilters();
    }
  }

  protected onPageSizeChange(newSize: number): void {
    this.pageSize = newSize;
    this.currentPage = 1; // Reset to first page when changing page size
    this.applyFilters();
  }
}
