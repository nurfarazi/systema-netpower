export interface User {
  id: number;
  name: string;
  email: string;
  isActive: boolean;
  createdAt: string;
  createdBy: string;
}

export interface UsersResponse {
  items: User[];
}
