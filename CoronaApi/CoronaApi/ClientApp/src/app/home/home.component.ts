import {Component, OnInit} from '@angular/core';
import {Course} from '../core/models/course';
import {CourseService} from '../core/services/course/course.service';
import {Exercise} from '../core/models/exercise';
import {ExerciseService} from '../core/services/exercise/exercise.service';
import {User, UserType} from '../core/models/user';
import {UserService} from '../core/services/user/user.service';
import {SubjectService} from "../core/services/subject/subject.service";
import {Subject} from "../core/models/subject";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  user: User;
  UserType = UserType;
  userCourses: Course[];
  userExercises: Exercise[];
  subjects: Subject[];

  constructor(
    private userService: UserService,
    private courseService: CourseService,
    private exerciseService: ExerciseService,
    private subjectService: SubjectService
  ) { }

  ngOnInit() {
    this.user = this.userService.getCurrentUser();
    this.loadData();
  }

  loadData() {
    if (this.user.userType == UserType.School) {
      this.subjectService.getAll().subscribe(subjects => {
        this.subjects = subjects;
      });
    }
    else {
      this.courseService.getUserCourses().subscribe(courses => {
        this.userCourses = courses;
      });
      this.exerciseService.getUserExercises().subscribe(exercises => {
        this.userExercises = exercises;
      });
    }
  }

}
