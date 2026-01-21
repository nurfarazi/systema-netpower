import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserService } from './user-service';
import { UsersResponse, User } from '../models/user.interface';
import { environment } from '../../environments/environment.development';

describe('UserService', () => {
  let service: UserService;
  let httpMock: HttpTestingController;

  const mockUsers: User[] = [
    {
      id: 1,
      name: 'John Doe',
      email: 'john@example.com',
      isActive: true,
      createdAt: '2024-01-21',
      createdBy: 'Admin'
    }
  ];

  const mockResponse: UsersResponse = {
    items: mockUsers
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [UserService]
    });
    service = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch users and update users$ observable', (done) => {
    service.refreshUsers();

    const req = httpMock.expectOne(`${environment.apiUrl}/Users`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);

    service.users$.subscribe((users) => {
      expect(users).toEqual(mockUsers);
      done();
    });
  });

  it('should set loading state during request', (done) => {
    service.refreshUsers();

    service.loading$.subscribe((loading) => {
      if (loading) {
        const req = httpMock.expectOne(`${environment.apiUrl}/Users`);
        req.flush(mockResponse);
      }
    });

    setTimeout(() => done(), 100);
  });

  it('should handle error gracefully', (done) => {
    service.refreshUsers();

    const req = httpMock.expectOne(`${environment.apiUrl}/Users`);
    req.error(new ErrorEvent('Network error'));

    service.users$.subscribe((users) => {
      expect(users).toEqual([]);
      done();
    });
  });
});
