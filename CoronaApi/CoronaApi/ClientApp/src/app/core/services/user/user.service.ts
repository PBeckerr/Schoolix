import { Injectable } from '@angular/core';
import { User, UserType } from '../../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }

  getCurrentUser(): User {
    return {
      id: '@example-user-01',
      name: 'Gräfin Erika Angelika von und zu Pflaume-Im-Speckmantel',
      userType: UserType.Teacher
    };
  }
}
