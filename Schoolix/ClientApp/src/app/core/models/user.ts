import { School } from './school';

export interface User {
  id: string;
  name: string;
  userType: UserType;
}

export enum UserType {
  'School', 'Teacher', 'Student'
}
