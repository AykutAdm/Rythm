export interface ResultUserDto {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  profileImageUrl:string;
  roles: string[];
}

export interface UserProfileDto {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  profileImageUrl: string;
  birthDate: string;
  createdAt: string;
  roles: string[];
}

export interface UpdateUserProfileDto {
  id: number;
  firstName: string;
  lastName: string;
  profileImageUrl: string;
  birthDate: string;
}
