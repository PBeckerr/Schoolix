import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Course } from '../core/models/course';
import { CourseService } from '../core/services/user/course.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss']
})
export class CourseComponent implements OnInit {

  course: Course;

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.courseService.getCourse(params['id']).subscribe(course => {
        this.course = course;
      });
    });
  }

}
