import { Component, Input, OnInit } from '@angular/core';
import { Exercise } from '../core/models/exercise';

@Component({
  selector: 'app-exercises',
  templateUrl: './exercises.component.html',
  styleUrls: ['./exercises.component.scss']
})
export class ExercisesComponent implements OnInit {

  @Input() exercises: Exercise[];

  constructor() { }

  ngOnInit() {

  }

}
