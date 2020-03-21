import { Exercise } from './exercise';
import { SchoolClass } from './schoolClass';
import { User } from './user';

export interface Course {
  id: string;
  schoolClasses: SchoolClass[];
  students: User[];
  name: string;
  exercises: Exercise[];
  teacher: User;
}
