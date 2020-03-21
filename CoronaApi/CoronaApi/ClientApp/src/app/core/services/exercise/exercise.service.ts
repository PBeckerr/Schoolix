import { Course } from '../../models/course';
import { Exercise } from '../../models/exercise';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { UserType } from '../../models/user';

@Injectable({
  providedIn: 'root'
})
export class ExerciseService {

  exercises: Exercise[] = [
    {
      id: '@example-exercise-01',
      name: 'Erste Beispielaufgabe',
      description: 'Diese Aufgabe ist <b>sehr</b> wichtig!',
      expirationDate: '2020-05-05',
      files: [
        {
          id: '@example-file-01',
          name: 'datei1.pdf',
          url: 'https://example.com/files/datei1.pdf'
        },
        {
          id: '@example-file-02',
          name: 'datei2.png',
          url: 'https://example.com/files/datei2.png'
        }
      ]
    }
  ];

  constructor() { }

  getExercise(id: string): Observable<Exercise> {
    return of(this.exercises.find(exercise => exercise.id === id));
  }

  getUserExercises(): Observable<Exercise[]> {
    return of(this.exercises);
  }

}
