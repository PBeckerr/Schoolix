import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Course } from '../core/models/course';
import { CourseService } from '../core/services/course/course.service';
import { UserService } from '../core/services/user/user.service';
import { UserType } from '../core/models/user';
import {AuthorizeService} from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss']
})
export class CourseComponent implements OnInit {

  course: Course;
  userType: UserType;
  userName: string;
  UserType = UserType;
  private authService: AuthorizeService;

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute,
    private userService: UserService,
    private authorizeService: AuthorizeService
  ) {
    this.userType = userService.getCurrentUser().userType;
    this.authService = authorizeService;
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.courseService.getCourse(params['id']).subscribe(course => {
        this.course = course;
      });
    });

    this.authService.getUser().subscribe(user => {
      this.userType = user.userType;
      this.userName = user.userName;
    });
  }

}
