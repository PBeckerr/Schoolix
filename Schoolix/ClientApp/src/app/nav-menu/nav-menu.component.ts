import {Component, OnInit} from '@angular/core';
import { User } from '../core/models/user';
import { UserService } from '../core/services/user/user.service';
import {AuthorizeService} from '../../api-authorization/authorize.service';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  user: User;
  private userName: Observable<string>;
  private isAuthenticated: Observable<boolean>;

  constructor(
    private userService: UserService,
    private authorizeService: AuthorizeService,
  ) {
    this.user = this.userService.getCurrentUser();
  }

  ngOnInit() {
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.userName));
    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }
}
