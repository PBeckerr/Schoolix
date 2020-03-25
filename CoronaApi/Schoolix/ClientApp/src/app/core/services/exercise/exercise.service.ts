import { Course } from '../../models/course';
import { Exercise } from '../../models/exercise';
import {Inject, Injectable} from '@angular/core';
import { Observable, of } from 'rxjs';
import { UserType } from '../../models/user';
import {HttpClient} from "@angular/common/http";
import {environment} from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ExerciseService {

  exercisesUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.exercisesUrl = environment.apiExercises;
    if (environment.apiUseBaseUrlAsPrefix) {
      this.exercisesUrl = baseUrl + this.exercisesUrl;
    }
  }

  getExercise(id: string): Observable<Exercise> {
    return this.http.get<Exercise>(this.exercisesUrl + '/' + id);
  }

  getUserExercises(): Observable<Exercise[]> {
    return this.http.get<Exercise[]>(this.exercisesUrl + '/my');
  }

  create(exercise: Exercise): Observable<Exercise> {
    return this.http.post<Exercise>(this.exercisesUrl, exercise);
  }

  update(exercise: Exercise): Observable<Exercise> {
    return this.http.put<Exercise>(this.exercisesUrl + '/' + exercise.id, exercise);
  }
}
