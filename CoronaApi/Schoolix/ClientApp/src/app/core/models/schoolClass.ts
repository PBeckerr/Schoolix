import { User } from './user';

export interface SchoolClass {
  id: string;
  grade: number;
  label: string;
  teacher: User;
  students: User[];
}
