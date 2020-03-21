import { Component } from '@angular/core';
import { User } from '../core/models/user';
import { UserService } from '../core/services/user/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {

  user: User;

  constructor(
    private userService: UserService
  ) {
    this.user = this.userService.getCurrentUser();
  }

}
