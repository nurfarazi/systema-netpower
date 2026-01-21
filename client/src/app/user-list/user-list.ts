import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserService } from '../services/user-service.js';
import { User } from '../models/user.interface';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-list.html',
  styleUrl: './user-list.scss'
})
export class UserList implements OnInit {
  private userService = inject(UserService);

  protected users$: Observable<User[]>;
  protected loading$: Observable<boolean>;

  constructor() {
    this.users$ = this.userService.users$;
    this.loading$ = this.userService.loading$;
  }

  ngOnInit(): void {
    this.userService.refreshUsers();
  }

  protected trackByUserId(index: number, user: User): number {
    return user.id;
  }
}
