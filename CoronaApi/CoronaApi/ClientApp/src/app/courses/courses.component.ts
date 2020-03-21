import { Component, Input, OnInit } from '@angular/core';
import { Course } from '../core/models/course';
import { Router } from '@angular/router';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit {

  @Input() courses: Course[];

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
  }

  routeCourse(id: string) {
    this.router.navigate(['/course', id]);
  }

}
