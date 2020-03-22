import {Component, OnInit} from '@angular/core';
import { User } from '../core/models/user';
import { UserService } from '../core/services/user/user.service';
import {AuthorizeService} from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  user: User;
  private userName: string;

  constructor(
    private userService: UserService,
    private authorizeService: AuthorizeService,
  ) {
    this.user = this.userService.getCurrentUser();
  }

  ngOnInit() {
    this.authorizeService.getUser().subscribe(value => {
      this.userName = value.userName;
    });
  }
}
