import { Component, Input, OnInit } from '@angular/core';
import { Course } from '../core/models/course';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit {

  @Input() courses: Course[];

  constructor() { }

  ngOnInit() {
  }

  routeCourse(id: string) {

  }

}
