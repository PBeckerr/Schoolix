import { Component, Input, OnInit } from '@angular/core';
import { Exercise } from '../core/models/exercise';
import { UserService } from '../core/services/user/user.service';
import { UserType } from '../core/models/user';

@Component({
  selector: 'app-exercises',
  templateUrl: './exercises.component.html',
  styleUrls: ['./exercises.component.scss']
})
export class ExercisesComponent implements OnInit {

  @Input() exercises: Exercise[];
  userType: UserType;
  UserType = UserType;
  files: File[];

  constructor(
    private userService: UserService
  ) { }

  ngOnInit() {
    this.userType = this.userService.getCurrentUser().userType;
  }

  addFileForUpload(event: any) {
    this.files = event.files;
  }

}
