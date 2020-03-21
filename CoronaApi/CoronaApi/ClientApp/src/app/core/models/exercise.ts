import { File } from './file';

export interface Exercise {
  id: string;
  name: string;
  description: string;
  expirationDate: string;
  files: File[];
}
