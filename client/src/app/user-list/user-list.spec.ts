import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserList } from './user-list';
import { UserService } from '../services/user-service';
import { BehaviorSubject } from 'rxjs';
import { User } from '../models/user.interface';

describe('UserList', () => {
  let component: UserList;
  let fixture: ComponentFixture<UserList>;
  let userService: jasmine.SpyObj<UserService>;

  const mockUsers: User[] = [
    {
      id: 1,
      name: 'John Doe',
      email: 'john@example.com',
      isActive: true,
      createdAt: '2024-01-21',
      createdBy: 'Admin'
    },
    {
      id: 2,
      name: 'Jane Smith',
      email: 'jane@example.com',
      isActive: false,
      createdAt: '2024-01-20',
      createdBy: 'Admin'
    }
  ];

  beforeEach(async () => {
    const userServiceSpy = jasmine.createSpyObj('UserService', ['refreshUsers'], {
      users$: new BehaviorSubject<User[]>(mockUsers),
      loading$: new BehaviorSubject<boolean>(false)
    });

    await TestBed.configureTestingModule({
      imports: [UserList],
      providers: [{ provide: UserService, useValue: userServiceSpy }]
    }).compileComponents();

    userService = TestBed.inject(UserService) as jasmine.SpyObj<UserService>;
    fixture = TestBed.createComponent(UserList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call refreshUsers on init', () => {
    expect(userService.refreshUsers).toHaveBeenCalled();
  });

  it('should expose users$ observable', (done) => {
    component.users$.subscribe((users) => {
      expect(users).toEqual(mockUsers);
      done();
    });
  });

  it('should expose loading$ observable', (done) => {
    component.loading$.subscribe((loading) => {
      expect(loading).toBe(false);
      done();
    });
  });

  it('trackByUserId should return user id', () => {
    const user = mockUsers[0];
    expect(component['trackByUserId'](0, user)).toBe(1);
  });
});
