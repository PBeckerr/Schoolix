import { Component, Input, OnInit } from '@angular/core';
import { Course } from '../core/models/course';
import { Router } from '@angular/router';
import { UserService } from '../core/services/user/user.service';
import { UserType } from '../core/models/user';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit {

  @Input() courses: Course[];
  userType: UserType;
  UserType = UserType;

  constructor(
    private router: Router,
    private userService: UserService
  ) {
    this.userType = userService.getCurrentUser().userType;
  }

  ngOnInit() {
  }

  routeCourse(id: string) {
    this.router.navigate(['/course', id]);
  }

}
