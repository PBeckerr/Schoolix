import { SchoolClass } from './schoolClass';

export interface SchoolYear {
  id: string;
  begin: string;
  end: string;
  name: string;
  schoolClasses: SchoolClass[];
}
