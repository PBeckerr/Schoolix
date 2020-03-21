import { Component, OnInit } from '@angular/core';
import { User, UserType } from '../core/models/user';
import { UserService } from '../core/services/user/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  user: User;
  UserType = UserType;

  constructor(
    private userService: UserService
  ) { }

  ngOnInit() {
    this.user = this.userService.getCurrentUser();
  }

}
