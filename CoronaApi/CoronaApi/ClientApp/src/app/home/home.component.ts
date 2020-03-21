import { Component, OnInit } from '@angular/core';
import { Course } from '../core/models/course';
import { CourseService } from '../core/services/user/course.service';
import { User, UserType } from '../core/models/user';
import { UserService } from '../core/services/user/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  user: User;
  UserType = UserType;
  userCourses: Course[];

  constructor(
    private userService: UserService,
    private courseService: CourseService
  ) { }

  ngOnInit() {
    this.user = this.userService.getCurrentUser();
    this.loadData();
  }

  loadData() {
    this.courseService.getUserCourses().subscribe(courses => {
      this.userCourses = courses;
    });
  }

}
