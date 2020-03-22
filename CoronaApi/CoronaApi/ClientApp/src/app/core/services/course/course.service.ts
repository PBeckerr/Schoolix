import { Course } from '../../models/course';
import {Inject, Injectable} from '@angular/core';
import { Observable, of } from 'rxjs';
import { UserService } from '../user/user.service';
import { UserType } from '../../models/user';
import {HttpClient} from "@angular/common/http";
import {environment} from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private courseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.courseUrl = environment.apiCourses;
    if (environment.apiUseBaseUrlAsPrefix) {
      this.courseUrl = baseUrl + this.courseUrl;
    }
  }

  getCourse(id: string): Observable<Course> {
    return this.http.get<Course>(this.courseUrl + '/' + id);
  }

  getUserCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.courseUrl + '/' +environment.apiMyCourses);
  }
}
