import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {Observable} from "rxjs";
import {Course} from "../../models/course";
import {Subject} from "../../models/subject";

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  private subjectsUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.subjectsUrl = environment.apiSubjects;
    if (environment.apiUseBaseUrlAsPrefix) {
      this.subjectsUrl = baseUrl + this.subjectsUrl;
    }
  }

  get(id: string): Observable<Subject> {
    return this.http.get<Subject>(this.subjectsUrl + '/' + id);
  }

  getAll(): Observable<Subject[]> {
    return this.http.get<Subject[]>(this.subjectsUrl);
  }

  create(course: Subject): Observable<Subject> {
    return this.http.post<Subject>(this.subjectsUrl, course);
  }

  update(course: Course): Observable<Course> {
    return this.http.put<Course>(this.subjectsUrl + '/' + course.id, course);
  }
}
