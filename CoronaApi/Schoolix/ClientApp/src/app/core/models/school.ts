import { SchoolYear } from './schoolYear';

export interface School {
  id: string;
  name: string;
  schoolYears: SchoolYear[];
  address: string;
  contact: string;
}
