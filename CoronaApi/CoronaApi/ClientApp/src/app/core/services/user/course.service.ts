import { Course } from '../../models/course';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { UserService } from './user.service';
import { UserType } from '../../models/user';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor() { }

  getUserCourses(): Observable<Course[]> {
    return of([
      {
        id: '@example-course-01',
        schoolClasses: [],
        students: [{
          id: '@example-user-01',
          name: 'Max Musterschüler',
          userType: UserType.Student
        }],
        name: 'LK Mathe Kl. 12',
        exercises: [],
        teacher: {
          id: '@example-teacher-01',
          name: 'Frau Lehrerin',
          userType: UserType.Teacher
        },
        subject: {
          id: 'MA',
          name: 'Mathematik'
        }
      },
      {
        id: '@example-course-02',
        schoolClasses: [],
        students: [{
          id: '@example-user-01',
          name: 'Max Musterschüler',
          userType: UserType.Student
        }],
        name: 'GK Deutsch Kl. 12',
        exercises: [],
        teacher: {
          id: '@example-teacher-01',
          name: 'Frau Lehrerin',
          userType: UserType.Teacher
        },
        subject: {
          id: 'DE',
          name: 'Deutsch für Muttersprachler'
        }
      }
    ]);
  }

}
