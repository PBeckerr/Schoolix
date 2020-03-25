import { Component, OnInit } from '@angular/core';
import {Subject} from "../core/models/subject";
import {AuthorizeService} from "../../api-authorization/authorize.service";
import {SubjectService} from "../core/services/subject/subject.service";
import {ActivatedRoute} from "@angular/router";
import { UserType } from '../core/models/user';
import {UserService} from "../core/services/user/user.service";

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css']
})
export class SubjectComponent implements OnInit {

  subject: Subject;
  userType: UserType;
  userName: string;
  UserType = UserType;
  private authService: AuthorizeService;

  constructor(
    private subjectService: SubjectService,
    private route: ActivatedRoute,
    private userService: UserService,
    private authorizeService: AuthorizeService
  ) {
    this.userType = userService.getCurrentUser().userType;
    this.authService = authorizeService;
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.subjectService.get(params['id']).subscribe(subject => {
        this.subject = subject;
      });
    });

    this.authService.getUser().subscribe(user => {
      this.userType = user.userType;
      this.userName = user.userName;
    });
  }
}
