import { Exercise } from './exercise';
import { File } from './file';
import { User } from './user';

export interface Submission {
  id: string;
  exercise: Exercise;
  student: User;
  files: File[];
  date: string;
}
